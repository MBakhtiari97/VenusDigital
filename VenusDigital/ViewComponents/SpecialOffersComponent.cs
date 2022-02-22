using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;

namespace VenusDigital.ViewComponents
{
    public class SpecialOffersComponent:ViewComponent
    {
        private IProductsRepository _productsRepository;

        public SpecialOffersComponent(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        #region SpecialOffers

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/ViewComponents/_SpecialOffers.cshtml", _productsRepository.GetSpecialOffers());

        }

        #endregion
    }
}
