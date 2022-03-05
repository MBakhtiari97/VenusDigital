using System;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;
using VenusDigital.Models;

namespace VenusDigital.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        #region Injection

        private IOrderRepository _orderRepository;
        private IProductsRepository _productsRepository;
        public INotyfService _notiService;

        public CartController(IOrderRepository orderRepository, IProductsRepository productsRepository, INotyfService notiService)
        {
            _orderRepository = orderRepository;
            _productsRepository = productsRepository;
            _notiService = notiService;
        }

        #endregion

        #region ShowCart

        [Route("Cart")]
        public IActionResult ShowCart()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            return View(_orderRepository.GetOrderByUserId(userId));
        }

        #endregion

        #region AddToCart

        public IActionResult AddToCart(int productId, string? color="")
        {
            var product = _productsRepository.GetProductForCart(productId);

            if (product != null)
            {
                int userId = int.Parse
                    (User.FindFirstValue(ClaimTypes.NameIdentifier)
                        .ToString());

                var order = _orderRepository.
                    GetOrderByUserId(userId);
                if (order != null)
                {
                    var orderDetail = _orderRepository
                        .GetOrderDetails(order.OrderId, product.ProductId);

                    if (orderDetail != null)
                    {
                        orderDetail.Count += 1;

                        //Increasing order price
                        if (product.ProductOnSalePrice != 0)
                        {
                            order.TotalOrderPrice += product.ProductOnSalePrice;
                        }
                        else
                        {
                            order.TotalOrderPrice += product.ProductMainPrice;
                        }
                    }
                    else
                    {
                        OrderDetails newDetails = new OrderDetails()
                        {
                            ProductId = product.ProductId,
                            Count = 1,
                            OrderId = order.OrderId
                        };
                        if (product.ProductOnSalePrice != 0)
                        {
                            order.TotalOrderPrice += product.ProductOnSalePrice;
                        }
                        else
                        {
                            order.TotalOrderPrice += product.ProductMainPrice;
                        }
                        _orderRepository.AddOrderDetails(newDetails);
                    }

                }

                else
                {
                    Order newOrder = new Order()
                    {
                        CreateDate = DateTime.Now,
                        IsFinally = false,
                        UserId = userId
                    };
                    _orderRepository.AddOrder(newOrder);
                    _orderRepository.AddOrderDetails(new OrderDetails()
                    {
                        ProductId = product.ProductId,
                        Count = 1,
                        OrderId = newOrder.OrderId
                    });
                }

                _orderRepository.SaveChanges();
            }
            _notiService.Success("Product Added To Your Cart !");
            return RedirectToAction("ShowCart");
        }


        #endregion

        #region RemoveFromCart

        public IActionResult RemoveFromCart(int detailId)
        {
            //Getting UserId , Order and Product For Total Price
            int userId = int.Parse
            (User.FindFirstValue(ClaimTypes.NameIdentifier)
                .ToString());

            var order = _orderRepository.
                GetOrderByUserId(userId);


            var orderDetail = _orderRepository
                .getOrderDetail(detailId);

            var product = _productsRepository.GetProductForCart(orderDetail.ProductId);


            if (orderDetail.Count <= 1)
            {
                _orderRepository.RemoveOrderDetail(orderDetail);

                //Decreasing order price
                if (product.ProductOnSalePrice != 0)
                {
                    order.TotalOrderPrice -= product.ProductOnSalePrice;
                }
                else
                {
                    order.TotalOrderPrice -= product.ProductMainPrice;
                }
                _notiService.Information("Item Removed From Your Cart");
            }



            else if (orderDetail.Count > 1)
            {
                orderDetail.Count -= 1;

                if (product.ProductOnSalePrice != 0)
                {
                    order.TotalOrderPrice -= product.ProductOnSalePrice;
                }
                else
                {
                    order.TotalOrderPrice -= product.ProductMainPrice;
                }
                _notiService.Information("Item Quantity Has Decreased");
            }

            _orderRepository.SaveChanges();
            return RedirectToAction("ShowCart");
        }


        public IActionResult EmptyCart(int orderId)
        {
            //Getting UserId , Order For Total Price
            int userId = int.Parse
            (User.FindFirstValue(ClaimTypes.NameIdentifier)
                .ToString());

            var order = _orderRepository.
                GetOrderByUserId(userId);

            if (order != null)
            {
                order.TotalOrderPrice = 0;
                _orderRepository.SaveChanges();
            }

            _orderRepository.EmptyCart(orderId);
            _notiService.Information("Your Is Empty Now");
            return RedirectToAction("ShowCart");
        }
        #endregion

    }
}
