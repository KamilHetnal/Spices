using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spices.Data;
using spices.Models;
using spices.Models.ViewModels;
using spices.Utility;

namespace spices.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly IEmailSender _emailSender;
        private ApplicationDbContext _db;
        private int PageSize = 7;
        public OrderController(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        [Authorize]
        public async Task<IActionResult> Confirm(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            OrderDetailsVeiwModel orderDetailsVeiwModel = new OrderDetailsVeiwModel()
            {
                OrderHeader = await _db.OrderHeader.Include(o => o.ApplicationUser)
                    .FirstOrDefaultAsync(o => o.Id == id && o.UserId == claim.Value),
                OrderDetails = await _db.OrderDetails.Where(o => o.OrderId == id).ToListAsync()
            };

            return View(orderDetailsVeiwModel);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> OrderHistory(int productPage = 1)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            OrderListViewModel orderListVM = new OrderListViewModel()
            {
                Orders = new List<OrderDetailsVeiwModel>()
            };



            List<OrderHeader> OrderHeaderList = await _db.OrderHeader.Include(o => o.ApplicationUser).Where(u => u.UserId == claim.Value).ToListAsync();

            foreach (OrderHeader item in OrderHeaderList)
            {
                OrderDetailsVeiwModel individual = new OrderDetailsVeiwModel
                {
                    OrderHeader = item,
                    OrderDetails = await _db.OrderDetails.Where(o => o.OrderId == item.Id).ToListAsync()
                };
                orderListVM.Orders.Add(individual);
            }

            var count = orderListVM.Orders.Count;
            orderListVM.Orders = orderListVM.Orders.OrderByDescending(p => p.OrderHeader.Id)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize).ToList();

            orderListVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = count,
                urlParam = "/Customer/Order/OrderHistory?productPage=:"
            };

            return View(orderListVM);
        }

        [Authorize]
        public async Task<IActionResult> OrderPickup(int productPage = 1, string searchEmail = null,
            string searchName = null, string searchPhone = null)
        {
            //var claimsIdentity = (ClaimsIdentity)User.Identity;
            //var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            OrderListViewModel orderListVM = new OrderListViewModel()
            {
                Orders = new List<OrderDetailsVeiwModel>()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Customer/Order/OrderHistory?productPage=:");
            param.Append("&searchName=");
            if (searchName != null)
            {
                param.Append(searchName);
            }

            param.Append("&s=searchPhone");
            if (searchPhone != null)
            {
                param.Append(searchPhone);
            }

            param.Append("&searchName=searchEmail");
            if (searchEmail != null)
            {
                param.Append(searchEmail);
            }

            List<OrderHeader> OrderHeaderList = new List<OrderHeader>();

            if (searchName != null || searchPhone != null || searchEmail != null)
            {
                var User = new ApplicationUser();
                OrderHeaderList = new List<OrderHeader>();
                if (searchName != null)
                {
                    OrderHeaderList = await _db.OrderHeader.Include(o => o.ApplicationUser)
                        .Where(u => u.Pickupname.ToLower().Contains(searchName.ToLower()) && u.Status == SD.StatusReady)
                        .OrderBy(o => o.OrderDate).ToListAsync();
                }
                else
                {
                    if (searchEmail != null)
                    {
                        User = await _db.ApplicationUsers.Where(u => u.Email.ToLower().Contains(searchEmail.ToLower()))
                            .FirstOrDefaultAsync();
                        OrderHeaderList = await _db.OrderHeader.Include(o => o.ApplicationUser)
                            .Where(u => u.UserId == User.Id && u.Status == SD.StatusReady)
                            .OrderByDescending(o => o.OrderDate).ToListAsync();
                    }
                    else
                    {
                        if (searchPhone != null)
                        {
                            OrderHeaderList = await _db.OrderHeader.Include(o => o.ApplicationUser)
                                .Where(u => u.PhoneNumber.Contains(searchPhone.ToLower()) && u.Status == SD.StatusReady)
                                .OrderBy(o => o.OrderDate).ToListAsync();
                        }
                    }
                }
            }
            else
            {
                OrderHeaderList = await _db.OrderHeader.Include(o => o.ApplicationUser).Where(u => u.Status == SD.StatusReady).ToListAsync();
            }

            foreach (OrderHeader item in OrderHeaderList)
            {
                OrderDetailsVeiwModel individual = new OrderDetailsVeiwModel
                {
                    OrderHeader = item,
                    OrderDetails = await _db.OrderDetails.Where(o => o.OrderId == item.Id).ToListAsync()
                };
                orderListVM.Orders.Add(individual);
            }

            var count = orderListVM.Orders.Count;
            orderListVM.Orders = orderListVM.Orders.OrderByDescending(p => p.OrderHeader.Id)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize).ToList();

            orderListVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItem = count,
                urlParam = param.ToString()
            };

            return View(orderListVM);
        }

        [Authorize(Roles = SD.ManagerUser + "," + SD.FrontDeskUser)]
        [HttpPost]
        [ActionName("orderPickup")]
        public async Task<IActionResult> OrderPickupPost(int OrderId)
        {
            OrderHeader orderHeader = await _db.OrderHeader.FindAsync(OrderId);
            orderHeader.Status = SD.StatusComplited;
            await _db.SaveChangesAsync();

            await _emailSender.SendEmailAsync(_db.Users.Where(u => u.Id == orderHeader.UserId).FirstOrDefault().Email,
                $"Spice - Order Complited {orderHeader.Id.ToString()}", "Order has been complited successfully");
            return RedirectToAction("OrderPickup", "Order");
        }
        [Authorize(Roles = SD.KitchenUser + "," + SD.ManagerUser)]
        public async Task<IActionResult> ManageOrder()
        {
            List<OrderDetailsVeiwModel> orderDetailsVeiwModel = new List<OrderDetailsVeiwModel>();

            List<OrderHeader> OrderHeaderList = await _db.OrderHeader
                .Where(u => u.Status == SD.StatusSubmitted || u.Status == SD.StatusInProgress).OrderByDescending(o => o.PickUpTime).ToListAsync();

            foreach (OrderHeader item in OrderHeaderList)
            {
                OrderDetailsVeiwModel individual = new OrderDetailsVeiwModel()
                {
                    OrderHeader = item,
                    OrderDetails = await _db.OrderDetails.Where(o => o.OrderId == item.Id).ToListAsync()
                };
                orderDetailsVeiwModel.Add(individual);
            }


            return View(orderDetailsVeiwModel.OrderBy(o => o.OrderHeader.PickUpTime).ToList());
        }

        public async Task<IActionResult> GetOrderDetails(int Id)
        {
            OrderDetailsVeiwModel orderDetailsVeiwModel = new OrderDetailsVeiwModel()
            {
                OrderHeader = await _db.OrderHeader.FirstOrDefaultAsync(m => m.Id == Id),
                OrderDetails = await _db.OrderDetails.Where(m => m.OrderId == Id).ToListAsync()
            };

            orderDetailsVeiwModel.OrderHeader.ApplicationUser =
                await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == orderDetailsVeiwModel.OrderHeader.UserId);

            return PartialView("_IndividualOrderDetails", orderDetailsVeiwModel);
        }

        public IActionResult GetOrderStatus(int Id)
        {

            return PartialView("_OrderStatus", _db.OrderHeader.Where(m => m.Id == Id).FirstOrDefault().Status);
        }

        [Authorize(Roles = SD.KitchenUser + "," + SD.ManagerUser)]
        public async Task<IActionResult> OrderPrepare(int OrderId)
        {
            OrderHeader orderHeader = await _db.OrderHeader.FindAsync(OrderId);
            orderHeader.Status = SD.StatusInProgress;
            await _db.SaveChangesAsync();
            return RedirectToAction("ManageOrder", "Order");
        }

        [Authorize(Roles = SD.KitchenUser + "," + SD.ManagerUser)]
        public async Task<IActionResult> OrderReady(int OrderId)
        {
            OrderHeader orderHeader = await _db.OrderHeader.FindAsync(OrderId);
            orderHeader.Status = SD.StatusReady;
            await _db.SaveChangesAsync();


            await _emailSender.SendEmailAsync(_db.Users.Where(u => u.Id == orderHeader.UserId).FirstOrDefault().Email,
                $"Spice - Order Ready {orderHeader.Id.ToString()}", "Order is ready for pickup");


            return RedirectToAction("ManageOrder", "Order");
        }

        [Authorize(Roles = SD.KitchenUser + "," + SD.ManagerUser)]
        public async Task<IActionResult> OrderCancelled(int OrderId)
        {
            OrderHeader orderHeader = await _db.OrderHeader.FindAsync(OrderId);
            orderHeader.Status = SD.StatusCancelled;
            await _db.SaveChangesAsync();

            await _emailSender.SendEmailAsync(_db.Users.Where(u => u.Id == orderHeader.UserId).FirstOrDefault().Email,
                $"Spice - Order Cancelled {orderHeader.Id.ToString()}", "Order has been cancelled successfully");

            return RedirectToAction("ManageOrder", "Order");
        }
    }
}