using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TECH.Areas.Admin.Models;
using TECH.Service;

namespace TECH.Controllers.Components

{
    [ViewComponent(Name = "CategoryMenuComponent")]
    public class CategoryMenuComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public CategoryMenuComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {           
            var categoryModel = _categoryService.GetAllMenu();
            var data = new List<CategoryModelView>();
            if (categoryModel != null && categoryModel.Count >0)
            {
                var categoryParent = categoryModel.Where(p => p.parentId == null).ToList();
                if (categoryParent != null && categoryParent.Count > 0)
                {
                    foreach (var item in categoryParent)
                    {
                        var itemChild = categoryModel.Where(p=>p.parentId == item.id).ToList();
                        item.LstChildrent = itemChild;
                        data.Add(item);
                    }
                }
            }
            return View(data);
        }
    }
}