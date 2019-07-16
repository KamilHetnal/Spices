using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using spices.Models;

namespace spices.Utility
{
    public static class SD
    {
        public const string DefaultFoodImage = "default_food.png";
        public const string ManagerUser = "Manager";
        public const string KitchenUser = "Kitchen";
        public const string FrontDeskUser = "FrontDesk";
        public const string CustomerUser = "Customer";
        public const string ssCardCount= "ssCardCount";
        public const string ssCouponCode= "ssCouponCode";

        public const string StatusSubmitted= "Submitted";
        public const string StatusInProgress= "Being Prepered";
        public const string StatusReady= "Ready for Pickup";
        public const string StatusComplited= "Complited";
        public const string StatusCancelled= "Cancelled";

        public const string PaymentStatusPending= "Pending";
        public const string PaymentStatusApproved = "Approved";
        public const string PaymentStatusRejected = "Rejected";



        

        public static string ConvertToRawHtml(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for(int i =0; i < source.Length;i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0 , arrayIndex);
        }

        public static double DiscountedPrice(Coupon couponFromDb, double OrginalOrderTotal)
        {
            if (couponFromDb == null)
            {
                return OrginalOrderTotal;
            }
            else
            {
                if (couponFromDb.MinimumAmount > OrginalOrderTotal)
                {
                    return OrginalOrderTotal;
                }
                else
                {
                    if(Convert.ToInt32(couponFromDb.CouponType) == (int)Coupon.ECouponType.Dollar)
                    {
                        return Math.Round(OrginalOrderTotal - couponFromDb.Discount, 2);
                    }
                    if (Convert.ToInt32(couponFromDb.CouponType) == (int)Coupon.ECouponType.Percent)
                    {
                        return Math.Round(OrginalOrderTotal - (OrginalOrderTotal*couponFromDb.Discount/100), 2);
                    }
                }
            }

            return OrginalOrderTotal;
        }
    }
}
