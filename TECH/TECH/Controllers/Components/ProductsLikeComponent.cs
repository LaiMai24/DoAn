using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TECH.Areas.Admin.Models;
using TECH.Service;

namespace TECH.Controllers.Components

{
    [ViewComponent(Name = "ProductsLikeComponent")]
    public class ProductsLikeComponent : ViewComponent
    {
        private readonly IProductsService _productService;
        public ProductsLikeComponent(IProductsService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int? categoryId)
        {
            var lstdata = new List<ProductModelView>();
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                var productRelate = _productService.GetProductLike(categoryId.Value);
                if (productRelate != null && productRelate.Count > 0)
                {
                    lstdata = productRelate.Where(p=>p.status != 1).ToList();
                }
            }
          
            return View(lstdata);
        }
    }
}