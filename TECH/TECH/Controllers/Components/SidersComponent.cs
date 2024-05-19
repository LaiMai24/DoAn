using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TECH.Areas.Admin.Models;
using TECH.Service;

namespace TECH.Controllers.Components

{
    [ViewComponent(Name = "SidersComponent")]
    public class SidersComponent : ViewComponent
    {
        private readonly ISidersService _sidersService;
        public SidersComponent(ISidersService sidersService)
        {
            _sidersService = sidersService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = _sidersService.GetImage();
            return View(data);
        }
    }
}