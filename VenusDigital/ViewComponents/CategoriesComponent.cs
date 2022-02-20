using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using VenusDigital.Data.Repositories;
using VenusDigital.Models.ViewModels;

namespace VenusDigital.ViewComponents
{
    public class CategoriesComponent:ViewComponent
    {
        private ICategoryRepository _categoryRepository;

        public CategoriesComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("/Views/ViewComponents/CategoriesComponent.cshtml",_categoryRepository.GetCategories());

        }
    }
}
