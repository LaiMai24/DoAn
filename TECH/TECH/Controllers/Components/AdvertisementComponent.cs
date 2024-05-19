using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TECH.Areas.Admin.Models;
using TECH.Service;

namespace TECH.Controllers.Components

{
    [ViewComponent(Name = "AdvertisementComponent")]
    public class AdvertisementComponent : ViewComponent
    {
        private readonly IAdvertisementService _dvertisementService;
        public AdvertisementComponent(IAdvertisementService dvertisementService)
        {
            _dvertisementService = dvertisementService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {           
            string image = _dvertisementService.GetImage();
            var category = new CategoryModelView();
            if (!string.IsNullOrEmpty(image))
            {
                category.name = image;
            }
           
            return View(category);
        }
    }
}