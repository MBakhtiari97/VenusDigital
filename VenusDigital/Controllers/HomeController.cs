using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using VenusDigital.Data.Repositories;
using VenusDigital.Models;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.Controllers
{
    public class HomeController : Controller
    {
        private IProductsRepository _productsRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger
            ,IProductsRepository productsRepository
            )
        {
            _logger = logger;
            _productsRepository = productsRepository;
        }

        //Showing New Items In Home Page
        public IActionResult Index()
        {
            var vm = new List<SingleProductViewModel>();

            foreach (var product in _productsRepository.GetNewProducts())
            {
                vm.Add(new SingleProductViewModel()
                {
                    MainImage = product.ProductGalleries.First().ImageName,
                    MainPrice = product.ProductMainPrice,
                    Score = product.ProductScore,
                    Title = product.ProductTitle,
                    ProductId = product.ProductId,
                    Quantiny = product.ProductQuantityInStock
                });
            }

            ViewBag.NewPhones = _productsRepository.GetNewPhonesProducts();
            ViewBag.NewHardware = _productsRepository.GetNewHardwareProducts();
            ViewBag.NewPcAccessories = _productsRepository.GetNewPcAccessoriesProducts();
            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Route("404")]
        public IActionResult NotFound()
        {
            return View("NotFoundPG");
        }
    }
}
