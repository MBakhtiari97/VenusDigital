using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;

namespace VenusDigital.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }
        public IActionResult ShowProductDetails()
        {
            //TODO:FILL THIS PART
            return View();
        }
    }
}
