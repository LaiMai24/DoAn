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
    [ViewComponent(Name = "ReviewSidersComponent")]
    public class ReviewSidersComponent : ViewComponent
    {
        //private readonly IAdvertisementService _dvertisementService;
        //public ReviewSidersComponent(IAdvertisementService dvertisementService)
        //{
        //    _dvertisementService = dvertisementService;
        //}


        private readonly IReviewsService _reviewsService;
        private readonly IProductsService _productService;
        private readonly IOrdersService _ordersService;
        private readonly IAppUserService _appUserService;
        public ReviewSidersComponent(
            IReviewsService reviewsService,
            IProductsService productService,
            IOrdersService ordersService,
            IAppUserService appUserService
            )
        {
            _reviewsService = reviewsService;
            _productService = productService;
            _appUserService = appUserService;
            _ordersService = ordersService;
        }




        public async Task<IViewComponentResult> InvokeAsync()
        {

            var reviewsViewModelSearch = new ReviewsViewModelSearch();
            reviewsViewModelSearch.PageIndex = 1;
            reviewsViewModelSearch.PageSize = 50;
            var data = _reviewsService.GetAllPaging(reviewsViewModelSearch);
            if (data.Results != null && data.Results.Count > 0)
            {
                data.Results = data.Results.Where(r => r.status != 1).ToList();
                foreach (var item in data.Results)
                {
                    if (item.order_id.HasValue && item.order_id.Value > 0)
                    {
                        var dataOrders = _ordersService.GetByid(item.order_id.Value);
                        if (dataOrders != null)
                        {
                            item.ordersModelView = dataOrders;

                            if (dataOrders.id > 0 && dataOrders.user_id.HasValue && dataOrders.user_id.Value > 0)
                            {
                                var dataUser = _appUserService.GetByid(dataOrders.user_id.Value);
                                if (dataUser != null)
                                {
                                    item.userModelView = dataUser;
                                }
                            }
                        }
                    }

                }
            }
            return View(data.Results.ToList());
        }
    }
}