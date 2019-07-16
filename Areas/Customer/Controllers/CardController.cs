using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spices.Data;
using spices.Models;
using spices.Models.ViewModels;
using spices.Utility;
using Stripe;

namespace spices.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CardController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public OrderDetailsCard detailCard { get; set; }

        public CardController(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }
        public async Task<IActionResult> Index()
        {
            detailCard = new OrderDetailsCard()
            {
                OrderHeader = new OrderHeader()
            };

            detailCard.OrderHeader.OrderTotal = 0;

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var card = _db.ShoppingCard.Where(c => c.ApplicationUserId == claim.Value);
            if (card != null)
            {
                detailCard.ListCard = card.ToList();
            }

            foreach (var list in detailCard.ListCard)
            {
                list.MenuItem = await _db.MenuItem.FirstOrDefaultAsync(m => m.Id == list.MenuItemId);
                detailCard.OrderHeader.OrderTotal =
                    detailCard.OrderHeader.OrderTotal + (list.MenuItem.Price * list.Count);
                list.MenuItem.Description = SD.ConvertToRawHtml(list.MenuItem.Description);
                if (list.MenuItem.Description.Length > 100)
                {
                    list.MenuItem.Description = list.MenuItem.Description.Substring(0, 99) + "...";
                }

            }
            detailCard.OrderHeader.OrderTotalOriginal = detailCard.OrderHeader.OrderTotal;

            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            {
                detailCard.OrderHeader.CouponCode = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await _db.Coupon
                    .Where(c => c.Name.ToLower() == detailCard.OrderHeader.CouponCode.ToLower()).FirstOrDefaultAsync();
                detailCard.OrderHeader.OrderTotal =
                    SD.DiscountedPrice(couponFromDb, detailCard.OrderHeader.OrderTotalOriginal);
            }
            return View(detailCard);
        }

        public async Task<IActionResult> Summary()
        {
            detailCard = new OrderDetailsCard()
            {
                OrderHeader = new OrderHeader()
            };

            detailCard.OrderHeader.OrderTotal = 0;

            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser applicationUser =
                await _db.ApplicationUsers.Where(c => c.Id == claim.Value).FirstOrDefaultAsync();
            var card = _db.ShoppingCard.Where(c => c.ApplicationUserId == claim.Value);
            if (card != null)
            {
                detailCard.ListCard = card.ToList();
            }

            foreach (var list in detailCard.ListCard)
            {
                list.MenuItem = await _db.MenuItem.FirstOrDefaultAsync(m => m.Id == list.MenuItemId);
                detailCard.OrderHeader.OrderTotal =
                    detailCard.OrderHeader.OrderTotal + (list.MenuItem.Price * list.Count);

            }
            detailCard.OrderHeader.OrderTotalOriginal = detailCard.OrderHeader.OrderTotal;

            detailCard.OrderHeader.Pickupname = applicationUser.Name;
            detailCard.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            detailCard.OrderHeader.PickUpTime = DateTime.Now;

            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            {
                detailCard.OrderHeader.CouponCode = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await _db.Coupon
                    .Where(c => c.Name.ToLower() == detailCard.OrderHeader.CouponCode.ToLower()).FirstOrDefaultAsync();
                detailCard.OrderHeader.OrderTotal =
                    SD.DiscountedPrice(couponFromDb, detailCard.OrderHeader.OrderTotalOriginal);
            }
            return View(detailCard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public async Task<IActionResult> SummaryPost(string stripeEmail, string stripeToken)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            detailCard.ListCard = await _db.ShoppingCard.Where(c => c.ApplicationUserId == claim.Value).ToListAsync();

            detailCard.OrderHeader.PeymentStatus = SD.PaymentStatusPending;
            detailCard.OrderHeader.OrderDate = DateTime.Now;
            detailCard.OrderHeader.UserId = claim.Value;
            detailCard.OrderHeader.Status = SD.PaymentStatusPending;
            detailCard.OrderHeader.PickUpTime = Convert.ToDateTime(detailCard.OrderHeader.PickUpDate.ToShortDateString() + " " + detailCard.OrderHeader.PickUpTime.ToShortTimeString());

            List<OrderDetails> orderDetailsList = new List<OrderDetails>();
            _db.OrderHeader.Add(detailCard.OrderHeader);
            await _db.SaveChangesAsync();

            detailCard.OrderHeader.OrderTotalOriginal = 0;


            foreach (var item in detailCard.ListCard)
            {
                item.MenuItem = await _db.MenuItem.FirstOrDefaultAsync(m => m.Id == item.MenuItemId);
                OrderDetails orderDetails = new OrderDetails()
                {
                    MenuItemId = item.MenuItemId,
                    OrderId = detailCard.OrderHeader.Id,
                    Description = item.MenuItem.Description,
                    Name = item.MenuItem.Name,
                    Price = item.MenuItem.Price,
                    Count = item.Count
                };

                detailCard.OrderHeader.OrderTotalOriginal += orderDetails.Count * orderDetails.Price;
                _db.OrderDetails.Add(orderDetails);

            }

            if (HttpContext.Session.GetString(SD.ssCouponCode) != null)
            {
                detailCard.OrderHeader.CouponCode = HttpContext.Session.GetString(SD.ssCouponCode);
                var couponFromDb = await _db.Coupon
                    .Where(c => c.Name.ToLower() == detailCard.OrderHeader.CouponCode.ToLower()).FirstOrDefaultAsync();
                detailCard.OrderHeader.OrderTotal =
                    SD.DiscountedPrice(couponFromDb, detailCard.OrderHeader.OrderTotalOriginal);
            }
            else
            {
                detailCard.OrderHeader.OrderTotal = detailCard.OrderHeader.OrderTotalOriginal;
            }

            detailCard.OrderHeader.CouponCodeDiscount =
                detailCard.OrderHeader.OrderTotalOriginal - detailCard.OrderHeader.OrderTotal;

            _db.ShoppingCard.RemoveRange(detailCard.ListCard);
            HttpContext.Session.SetInt32(SD.ssCardCount, 0);
            await _db.SaveChangesAsync();

            //Stripe Logic

            if (stripeToken != null)
            {

                var customers = new CustomerService();
                var charges = new ChargeService();

                var customer = customers.Create(new CustomerCreateOptions
                {
                    Email = stripeEmail,
                    Source = stripeToken
                });

                var charge = charges.Create(new ChargeCreateOptions
                {
                    Amount = Convert.ToInt32(detailCard.OrderHeader.OrderTotal * 100),
                    Description = "Order ID : " + detailCard.OrderHeader.Id,
                    Currency = "usd",
                    CustomerId = customer.Id
                });

                detailCard.OrderHeader.TransactionId = charge.BalanceTransactionId;
                if (charge.Status.ToLower() == "succeeded")
                {
                    await _emailSender.SendEmailAsync(_db.Users.Where(u => u.Id == claim.Value).FirstOrDefault().Email,
                        $"Spice - Order Created {detailCard.OrderHeader.Id.ToString()}", "Order has been submitted successfully");

                    detailCard.OrderHeader.PeymentStatus = SD.PaymentStatusApproved;
                    detailCard.OrderHeader.Status = SD.StatusSubmitted;
                }
                else
                {
                    detailCard.OrderHeader.PeymentStatus = SD.PaymentStatusRejected;
                }
            }
            else
            {
                detailCard.OrderHeader.PeymentStatus = SD.PaymentStatusRejected;
            }

            await _db.SaveChangesAsync();

            //return RedirectToAction("Index", "Home");

            return RedirectToAction("Confirm", "Order", new {id = detailCard.OrderHeader.Id});
        }

        public IActionResult AddCoupon()
        {
            if (detailCard.OrderHeader.CouponCode == null)
            {
                detailCard.OrderHeader.CouponCode = "";
            }
            HttpContext.Session.SetString(SD.ssCouponCode, detailCard.OrderHeader.CouponCode);

            return RedirectToAction(nameof(Index));
        }


        public IActionResult RemoveCoupon()
        {

            HttpContext.Session.SetString(SD.ssCouponCode, string.Empty);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Plus(int cardId)
        {
            var card = await _db.ShoppingCard.FirstOrDefaultAsync(c => c.Id == cardId);
            card.Count += 1;
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Minus(int cardId)
        {
            var card = await _db.ShoppingCard.FirstOrDefaultAsync(c => c.Id == cardId);
            if (card.Count == 1)
            {
                _db.ShoppingCard.Remove(card);
                await _db.SaveChangesAsync();

                var cnt = _db.ShoppingCard.Where(u => u.ApplicationUserId == card.ApplicationUserId).ToList().Count;
                HttpContext.Session.SetInt32(SD.ssCardCount, cnt);
            }

            else
            {
                card.Count -= 1;
                await _db.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(int cardId)
        {
            var card = await _db.ShoppingCard.FirstOrDefaultAsync(c => c.Id == cardId);

            _db.ShoppingCard.Remove(card);
            await _db.SaveChangesAsync();

            var cnt = _db.ShoppingCard.Where(u => u.ApplicationUserId == card.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ssCardCount, cnt);

            return RedirectToAction(nameof(Index));
        }


    }
}