using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;
using VenusDigital.Models;

namespace VenusDigital.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IOrderRepository _orderRepository;
        private IProductsRepository _productsRepository;

        public CartController(IOrderRepository orderRepository,IProductsRepository productsRepository)
        {
            _orderRepository = orderRepository;
            _productsRepository = productsRepository;   
        }

        #region ShowCart

        [Route("Cart")]
        public IActionResult ShowCart()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            return View(_orderRepository.GetOrderByUserId(userId));
        }

        #endregion

        #region AddToCart

        public IActionResult AddToCart(int productId)
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
                    }
                    else
                    {
                        OrderDetails newDetails = new OrderDetails()
                        {
                            ProductId = product.ProductId,
                            Count = 1,
                            OrderId = order.OrderId
                        };
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

            return RedirectToAction("ShowCart");
        }


        #endregion

        //TODO:Remove From Cart
        //TODO:FIX SHOW TOTAL PRICE VALUE
    }
}
