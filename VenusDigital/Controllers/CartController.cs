using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;

namespace VenusDigital.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IOrderRepository _orderRepository;

        public CartController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        [Route("Cart")]
        public IActionResult ShowCart()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            return View(_orderRepository.GetOrderByUserId(userId));
        }
    }
}
