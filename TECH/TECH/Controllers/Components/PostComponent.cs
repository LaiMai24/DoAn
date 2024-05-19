using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Service;

namespace TECH.Controllers.Components

{
    //[ViewComponent(Name = "CategoryMenuComponent")]
    public class PostComponent : ViewComponent
    {
        private readonly IPostsService _postsService;
        public PostComponent(IPostsService postsService)
        {
            _postsService = postsService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var postsViewModelSearch = new PostsViewModelSearch();
            postsViewModelSearch.PageIndex = 1;
            postsViewModelSearch.PageSize = 10;
            var data = _postsService.GetAllPaging(postsViewModelSearch);
            if (data != null && data.Results != null && data.Results.Count > 0)
            {
                data.Results = data.Results.Where(p => p.status != 1).ToList();
                //foreach (var item in data.Results)
                //{
                //    //if (item.author.HasValue)
                //    //{
                //    //    var appUser = _appUserService.GetByid(item.author.Value);
                //    //    if (appUser != null && !string.IsNullOrEmpty(appUser.full_name))
                //    //    {
                //    //        item.author_name = appUser.full_name;
                //    //    }
                //    //}

                //}
            }
            return View(data);
        }
    }
}