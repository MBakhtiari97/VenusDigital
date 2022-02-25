using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;

namespace VenusDigital.Controllers
{
    public class CouponsController : Controller
    {
        private ICouponRepository _couponRepository;
        private IOrderRepository _orderRepository;
        public INotyfService _notifyService { get; }

        public CouponsController(ICouponRepository couponRepository, INotyfService notifyService,IOrderRepository orderRepository)
        {
            _couponRepository = couponRepository;
            _notifyService = notifyService;
            _orderRepository = orderRepository;
        }


        public IActionResult ApplyCoupons()
        {
            return RedirectToAction("ShowCart","Cart");
        }

        [HttpPost]
        public IActionResult ApplyCoupons(string coupon)
        {
            var couponResult = _couponRepository
                .GetCoupons(coupon.ToUpper().Trim());

            if (couponResult != null)
            {
                if (couponResult.CouponCodeCount != 0)
                {
                    int userId = int.Parse
                    (User.FindFirstValue(ClaimTypes.NameIdentifier)
                        .ToString());

                    var order = _orderRepository.
                        GetOrderByUserId(userId);

                    if (!order.AppliedCoupon)
                    {
                        
                        order.AppliedCoupon = true;
                        if (order.TotalOrderPrice!=0)
                        {
                            if (couponResult.CouponPercent != 0)
                            {
                                order.TotalPriceWithCoupon = order.TotalOrderPrice - ((order.TotalOrderPrice * couponResult.CouponPercent) / 100);
                                couponResult.CouponCodeCount -= 1;
                                _notifyService.Success("Coupon Code Successfully Applied");
                            }
                            else if (couponResult.CouponValue != 0)
                            {
                                order.TotalPriceWithCoupon = order.TotalOrderPrice - couponResult.CouponValue;
                                couponResult.CouponCodeCount -= 1;
                                _notifyService.Success("Coupon Code Successfully Applied");
                            }
                            else if (couponResult.CouponValue != 0 && couponResult.CouponPercent != 0)
                            {
                                order.TotalPriceWithCoupon = order.TotalOrderPrice - ((order.TotalOrderPrice * couponResult.CouponPercent) / 100);
                                couponResult.CouponCodeCount -= 1;
                                _notifyService.Success("Coupon Code Successfully Applied");
                            }
                            else
                            {
                                _notifyService.Error("COUPON CODE IS NOT VALID !");
                            }
                            _orderRepository.SaveChanges();
                        }
                        else
                        {
                            _notifyService.Warning("Cart Is Empty");
                        }


                    }
                    else
                    {
                        _notifyService.Warning("You've Already Applied A Coupon !");
                    }
                    

                }
                else
                {
                    _notifyService.Warning("COUPON CODE HAS EXPIRED !");
                }
            }
            else
            {
                _notifyService.Error("COUPON CODE IS NOT VALID !");
            }

            return RedirectToAction("ShowCart", "Cart");
        }
    }
}
