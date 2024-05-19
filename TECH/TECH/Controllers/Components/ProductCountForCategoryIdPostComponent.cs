using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TECH.Areas.Admin.Models;
using TECH.Service;

namespace TECH.Controllers.Components

{
    [ViewComponent(Name = "ProductCountForCategoryIdPostComponent")]
    public class ProductCountForCategoryIdPostComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductsService _productService;
        public ProductCountForCategoryIdPostComponent(ICategoryService categoryService,
            IProductsService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryMenuCountModelView = new List<CategoryMenuCountModelView>();
            var categoryModel = _categoryService.GetAllMenu();
            if (categoryModel != null && categoryModel.Count() > 0)
            {
                categoryModel = categoryModel.Where(c => c.status != 1).ToList();
                foreach (var item in categoryModel)
                {
                    var countProduct = _productService.GetCountForCategory(item.id);
                    var _categoryMenuCountModelView = new CategoryMenuCountModelView()
                    {
                        id = item.id,
                        category_name = item.name,
                        count_product = countProduct,
                    };
                    categoryMenuCountModelView.Add(_categoryMenuCountModelView);
                }
            }
            return View(categoryMenuCountModelView);
        }
    }
}