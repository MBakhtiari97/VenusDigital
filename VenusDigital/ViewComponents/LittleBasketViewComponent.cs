using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;

namespace VenusDigital.ViewComponents
{
    public class LittleBasketViewComponent: ViewComponent
    {
        private IOrderRepository _orderRepository;

        public LittleBasketViewComponent(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            var user = User as ClaimsPrincipal;
           
            var userId = int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier).ToString());
            return View("/Views/ViewComponents/_ShowLittleBasketItems.cshtml", _orderRepository.GetOrderByUserId(userId));

        }

    }
}
