using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VenusDigital.Data.Repositories;

namespace VenusDigital.ViewComponents
{
    public class LeftSideCategoriesComponent:ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public LeftSideCategoriesComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/ViewComponents/LeftSideCategoriesComponent.cshtml", _categoryRepository.GetCategories());

        }
    }
}
