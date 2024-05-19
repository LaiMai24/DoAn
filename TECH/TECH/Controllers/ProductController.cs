using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Models;
using TECH.Service;
using TECH.Utilities;

namespace TECH.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly IImagesService _imagesService;
        private readonly IProductsImagesService _productsImagesService;
        private readonly ICategoryService _categoryService;
        private readonly IProductQuantityService _productQuantityService;
        private readonly ISizesService _sizesService;
        private readonly IReviewsService _reviewsService;
        public ProductController(IProductsService productsService,
            IImagesService imagesService,
            IProductsImagesService productsImagesService,
            IProductQuantityService productQuantityService,
            ISizesService sizesService,
            IReviewsService reviewsService,
        ICategoryService categoryService)
        {
            _productsService = productsService;
            _categoryService = categoryService;
            _imagesService = imagesService;
            _productsImagesService = productsImagesService;
            _productQuantityService = productQuantityService;
            _reviewsService = reviewsService;
            _sizesService = sizesService;
        }

        //public IActionResult ProductCategory(int categoryId)
        //{
        //    var productViewModelSearch = new ProductViewModelSearch();
        //    productViewModelSearch.PageIndex = 1;
        //    productViewModelSearch.PageSize = 250;

        //    productViewModelSearch.categoryId = categoryId;
        //    var data = _productsService.GetAllPaging(productViewModelSearch);
        //    if (data != null && data.Results != null && data.Results.Count > 0)
        //    {
        //        data.Results = data.Results.Where(p => p.ishidden != 1).ToList();
        //        foreach (var item in data.Results)
        //        {
        //            if (item.category_id.HasValue && item.category_id.Value > 0)
        //            {
        //                var category = _categoryService.GetByid(item.category_id.Value);
        //                var productimages = _productsImagesService.GetImageProduct(item.id);
        //                if (productimages != null && productimages.Count > 0)
        //                {
        //                    var lstImages = _imagesService.GetImageName(productimages);
        //                    if (lstImages != null && lstImages.Count > 0)
        //                    {
        //                        item.avatar = lstImages[0].name;
        //                    }
        //                }
        //                if (category != null && !string.IsNullOrEmpty(category.name))
        //                {
        //                    item.categorystr = category.name;
        //                }
        //                else
        //                {
        //                    item.categorystr = "Chờ xử lý";
        //                }
        //            }
        //            else
        //            {
        //                item.categorystr = "";
        //            }
        //            var productImage = _productsImagesService.GetImageProduct(item.id);
        //            if (productImage != null && productImage.Count > 0)
        //            {
        //                var image = _imagesService.GetImageName(productImage);
        //                if (image != null && image.Count > 0)
        //                {
        //                    item.ImageModelView = image;
        //                }
        //            }
        //            item.trademark = !string.IsNullOrEmpty(item.trademark) ? item.trademark : "";
        //            item.price_sell_str = item.price_sell.HasValue && item.price_sell.Value > 0 ? item.price_sell.Value.ToString("#,###") : "";
        //            item.price_import_str = item.price_import.HasValue && item.price_import.Value > 0 ? item.price_import.Value.ToString("#,###") : "";
        //            item.price_reduced_str = item.price_reduced.HasValue && item.price_reduced.Value > 0 ? item.price_reduced.Value.ToString("#,###") : "";
        //        }
        //    }
        //    return View(data.Results.ToList());
        //}

        public IActionResult ProductCategory(int categoryId, int sort)
        {
            var productViewModelSearch = new ProductViewModelSearch();
            productViewModelSearch.PageIndex = 1;
            productViewModelSearch.PageSize = 250;

            productViewModelSearch.categoryId = categoryId;
            var data = _productsService.GetAllPaging(productViewModelSearch);
            if (data != null && data.Results != null && data.Results.Count > 0)
            {
                data.Results = data.Results.Where(p => p.ishidden != 1).ToList();
                if (sort == 1)
                {
                    data.Results = data.Results.OrderBy(p => p.name).ToList();
                }
                else if (sort == 2)
                {
                    data.Results = data.Results.OrderByDescending(p => p.name).ToList();
                }
                else if (sort == 3)
                {
                    data.Results = data.Results.OrderBy(p => p.price_sell).ToList();
                }
                else if (sort == 4)
                {
                    data.Results = data.Results.OrderByDescending(p => p.price_sell).ToList();
                }
                foreach (var item in data.Results)
                {
                    var viewproduct = _reviewsService.GetReviewForProduct(item.id);
                    item.star = viewproduct.star;
                    if (item.category_id.HasValue && item.category_id.Value > 0)
                    {
                        var category = _categoryService.GetByid(item.category_id.Value);
                        var productimages = _productsImagesService.GetImageProduct(item.id);
                        if (productimages != null && productimages.Count > 0)
                        {
                            var lstImages = _imagesService.GetImageName(productimages);
                            if (lstImages != null && lstImages.Count > 0)
                            {
                                item.avatar = lstImages[0].name;
                            }
                        }
                        if (category != null && !string.IsNullOrEmpty(category.name))
                        {
                            item.categorystr = category.name;
                        }
                        else
                        {
                            item.categorystr = "Chờ xử lý";
                        }
                    }
                    else
                    {
                        item.categorystr = "";
                    }
                    var productImage = _productsImagesService.GetImageProduct(item.id);
                    if (productImage != null && productImage.Count > 0)
                    {
                        var image = _imagesService.GetImageName(productImage);
                        if (image != null && image.Count > 0)
                        {
                            item.ImageModelView = image;
                        }
                    }
                    item.trademark = !string.IsNullOrEmpty(item.trademark) ? item.trademark : "";
                    item.price_sell_str = item.price_sell.HasValue && item.price_sell.Value > 0 ? item.price_sell.Value.ToString("#,###") : "";
                    item.price_import_str = item.price_import.HasValue && item.price_import.Value > 0 ? item.price_import.Value.ToString("#,###") : "";
                    item.price_reduced_str = item.price_reduced.HasValue && item.price_reduced.Value > 0 ? item.price_reduced.Value.ToString("#,###") : "";
                    //item.total_product = 10;
                }
            }
            return View(data.Results.ToList());
        }

        public IActionResult ProductStatus(int status)
        {
            var productViewModelSearch = new ProductViewModelSearch();
            productViewModelSearch.PageIndex = 1;
            productViewModelSearch.PageSize = 50;

            productViewModelSearch.status = status;
            var data = _productsService.GetAllPaging(productViewModelSearch);
            if (data != null && data.Results != null && data.Results.Count > 0)
            {
                data.Results = data.Results.Where(p => p.ishidden != 1).ToList();
                foreach (var item in data.Results)
                {
                    if (item.category_id.HasValue && item.category_id.Value > 0)
                    {
                        var category = _categoryService.GetByid(item.category_id.Value);
                        var productimages = _productsImagesService.GetImageProduct(item.id);
                        if (productimages != null && productimages.Count > 0)
                        {
                            var lstImages = _imagesService.GetImageName(productimages);
                            if (lstImages != null && lstImages.Count > 0)
                            {
                                item.avatar = lstImages[0].name;
                            }
                        }
                        if (category != null && !string.IsNullOrEmpty(category.name))
                        {
                            item.categorystr = category.name;
                        }
                        else
                        {
                            item.categorystr = "Chờ xử lý";
                        }
                    }
                    else
                    {
                        item.categorystr = "";
                    }
                    var productImage = _productsImagesService.GetImageProduct(item.id);
                    if (productImage != null && productImage.Count > 0)
                    {
                        var image = _imagesService.GetImageName(productImage);
                        if (image != null && image.Count > 0)
                        {
                            item.ImageModelView = image;
                        }
                    }
                    item.trademark = !string.IsNullOrEmpty(item.trademark) ? item.trademark : "";
                    item.price_sell_str = item.price_sell.HasValue && item.price_sell.Value > 0 ? item.price_sell.Value.ToString("#,###") : "";
                    item.price_import_str = item.price_import.HasValue && item.price_import.Value > 0 ? item.price_import.Value.ToString("#,###") : "";
                    item.price_reduced_str = item.price_reduced.HasValue && item.price_reduced.Value > 0 ? item.price_reduced.Value.ToString("#,###") : "";
                    //item.total_product = 10;
                }
            }
            return View(data.Results.ToList());
        }

        public IActionResult ProductDetail(int productId)
        {
            var model = new ProductModelView();
            if (productId > 0)
            {
                model = _productsService.GetByid(productId);
                if (model != null && !string.IsNullOrEmpty(model.name))
                {
                    var viewproduct = _reviewsService.GetReviewForProduct(model.id);
                    model.star = viewproduct.star;
                    model.starOne = viewproduct.starOne;
                    model.startTwo = viewproduct.startTwo;
                    model.startThree = viewproduct.startThree;
                    model.startFour = viewproduct.startFour;
                    model.startFive = viewproduct.startFive;


                    model.starPercentOne = viewproduct.starPercentOne;
                    model.startPercentTwo = viewproduct.startPercentTwo;
                    model.startPercentThree = viewproduct.startPercentThree;
                    model.startPercentFour = viewproduct.startPercentFour;
                    model.startPercentFive = viewproduct.startPercentFive;


                    if (model.category_id.HasValue && model.category_id.Value > 0)
                    {
                        var category = _categoryService.GetByid(model.category_id.Value);
                        var productimages = _productsImagesService.GetImageProduct(model.id);
                        if (productimages != null && productimages.Count > 0)
                        {
                            var lstImages = _imagesService.GetImageName(productimages);
                            if (lstImages != null && lstImages.Count > 0)
                            {
                                model.avatar = lstImages[0].name;
                            }
                        }
                        if (category != null && !string.IsNullOrEmpty(category.name))
                        {
                            model.categorystr = category.name;
                        }
                        else
                        {
                            model.categorystr = "Chờ xử lý";
                        }

                    }
                    else
                    {
                        model.categorystr = "";
                    }
                    //var quantiyProduct = _productQuantityService.GetProductQuantity(model.id);
                    //if (quantiyProduct != null && quantiyProduct.Count > 0)
                    //{
                    //    var lstSizeModel = new List<SizesModelView>();
                    //    foreach (var itemSize in quantiyProduct)
                    //    {
                    //        if (itemSize.AppSizeId.HasValue && itemSize.AppSizeId.Value > 0)
                    //        {
                    //            var sizes = _sizesService.GetByid(itemSize.AppSizeId.Value);
                    //            if (sizes != null)
                    //            {
                    //                lstSizeModel.Add(sizes);
                    //            }
                    //        }
                    //    }
                    //    model.SizesModelView = lstSizeModel;
                    //}
                    var productImage = _productsImagesService.GetImageProduct(model.id);
                    if (productImage != null && productImage.Count > 0)
                    {
                        var image = _imagesService.GetImageName(productImage);
                        if (image != null && image.Count > 0)
                        {
                            model.ImageModelView = image;
                        }
                    }
                    model.trademark = !string.IsNullOrEmpty(model.trademark) ? model.trademark : "";
                    model.price_sell_str = model.price_sell.HasValue && model.price_sell.Value > 0 ? model.price_sell.Value.ToString("#,###") : "";
                    model.price_import_str = model.price_import.HasValue && model.price_import.Value > 0 ? model.price_import.Value.ToString("#,###") : "";
                    model.price_reduced_str = model.price_reduced.HasValue && model.price_reduced.Value > 0 ? model.price_reduced.Value.ToString("#,###") : "";


                }
            }
            return View(model);
        }


        public IActionResult ProductSearch(string textSearch)
        {
            var data = new PagedResult<ProductModelView>();
            var model = new List<ProductModelView>();
            if (!string.IsNullOrEmpty(textSearch))
            {
                var ProductModelViewSearch = new ProductViewModelSearch();
                ProductModelViewSearch.name = textSearch.Replace("timkiem=","");
                ProductModelViewSearch.PageIndex = 1;
                ProductModelViewSearch.PageSize = 20;
              
                data = _productsService.GetAllPaging(ProductModelViewSearch);
                if (data != null && data.Results != null && data.Results.Count > 0)
                {
                    data.Results = data.Results.Where(p => p.ishidden != 1).ToList();
                    foreach (var item in data.Results)
                    {
                        var viewproduct = _reviewsService.GetReviewForProduct(item.id);
                        item.star = viewproduct.star;
                        if (item.category_id.HasValue && item.category_id.Value > 0)
                        {
                            var category = _categoryService.GetByid(item.category_id.Value);
                            var productimages = _productsImagesService.GetImageProduct(item.id);
                            if (productimages != null && productimages.Count > 0)
                            {
                                var lstImages = _imagesService.GetImageName(productimages);
                                if (lstImages != null && lstImages.Count > 0)
                                {
                                    item.avatar = lstImages[0].name;
                                }
                            }
                            if (category != null && !string.IsNullOrEmpty(category.name))
                            {
                                item.categorystr = category.name;
                            }
                            else
                            {
                                item.categorystr = "Chờ xử lý";
                            }
                        }
                        else
                        {
                            item.categorystr = "";
                        }
                        var productImage = _productsImagesService.GetImageProduct(item.id);
                        if (productImage != null && productImage.Count > 0)
                        {
                            var image = _imagesService.GetImageName(productImage);
                            if (image != null && image.Count > 0)
                            {
                                item.ImageModelView = image;
                            }
                        }
                        item.trademark = !string.IsNullOrEmpty(item.trademark) ? item.trademark : "";
                        item.price_sell_str = item.price_sell.HasValue && item.price_sell.Value > 0 ? item.price_sell.Value.ToString("#,###") : "";
                        item.price_import_str = item.price_import.HasValue && item.price_import.Value > 0 ? item.price_import.Value.ToString("#,###") : "";
                        item.price_reduced_str = item.price_reduced.HasValue && item.price_reduced.Value > 0 ? item.price_reduced.Value.ToString("#,###") : "";
                        model.Add(item);
                    }
                }
            }
            return View(model);
        }      
        public IActionResult ProductHight(int sort)
        {
            var productModelViewSearch = new ProductViewModelSearch();
            productModelViewSearch.status = 2;            
            productModelViewSearch.PageIndex = 1;
            productModelViewSearch.PageSize = 100;
            var categoryModel = _productsService.GetAllPaging(productModelViewSearch);
            var model = new List<ProductModelView>();

            if (categoryModel != null && categoryModel.Results != null && categoryModel.Results.Count > 0)
            {
                categoryModel.Results = categoryModel.Results.Where(p => p.ishidden != 1).ToList();
                if (sort > 0)
                {
                    if (sort == 1)
                    {
                        categoryModel.Results = categoryModel.Results.OrderBy(p => p.name).ToList();
                    }else if (sort == 2)
                    {
                        categoryModel.Results = categoryModel.Results.OrderByDescending(p => p.name).ToList();
                    }
                    else if (sort == 3)
                    {
                        categoryModel.Results = categoryModel.Results.OrderBy(p => p.price_sell).ToList();
                    }
                    else
                    {
                        categoryModel.Results = categoryModel.Results.OrderByDescending(p => p.price_sell).ToList();
                    }
                }
               
                foreach (var item in categoryModel.Results)
                {
                    if (item.category_id.HasValue && item.category_id.Value > 0)
                    {
                        var category = _categoryService.GetByid(item.category_id.Value);
                        var productimages = _productsImagesService.GetImageProduct(item.id);
                        if (productimages != null && productimages.Count > 0)
                        {
                            var lstImages = _imagesService.GetImageName(productimages);
                            if (lstImages != null && lstImages.Count > 0)
                            {
                                item.avatar = lstImages[0].name;
                            }
                        }
                        if (category != null && !string.IsNullOrEmpty(category.name))
                        {
                            item.categorystr = category.name;
                        }
                        else
                        {
                            item.categorystr = "Chờ xử lý";
                        }
                    }
                    else
                    {
                        item.categorystr = "";
                    }
                    var productImage = _productsImagesService.GetImageProduct(item.id);
                    if (productImage != null && productImage.Count > 0)
                    {
                        var image = _imagesService.GetImageName(productImage);
                        if (image != null && image.Count > 0)
                        {
                            item.ImageModelView = image;
                        }
                    }
                    item.trademark = !string.IsNullOrEmpty(item.trademark) ? item.trademark : "";
                    item.price_sell_str = item.price_sell.HasValue && item.price_sell.Value > 0 ? item.price_sell.Value.ToString("#,###") : "";
                    item.price_import_str = item.price_import.HasValue && item.price_import.Value > 0 ? item.price_import.Value.ToString("#,###") : "";
                    item.price_reduced_str = item.price_reduced.HasValue && item.price_reduced.Value > 0 ? item.price_reduced.Value.ToString("#,###") : "";
                    model.Add(item);
                }
            }
            return View(model);
        }
        public IActionResult ProductNew(int sort)
        {
            var productModelViewSearch = new ProductViewModelSearch();
            productModelViewSearch.status = 1;
            productModelViewSearch.PageIndex = 1;
            productModelViewSearch.PageSize = 100;
            var categoryModel = _productsService.GetAllPaging(productModelViewSearch);
            var model = new List<ProductModelView>();
            if (categoryModel != null && categoryModel.Results != null && categoryModel.Results.Count > 0)
            {
                categoryModel.Results = categoryModel.Results.Where(p => p.ishidden != 1).ToList();
                if (sort > 0)
                {
                    if (sort == 1)
                    {
                        categoryModel.Results = categoryModel.Results.OrderBy(p => p.name).ToList();
                    }
                    else if (sort == 2)
                    {
                        categoryModel.Results = categoryModel.Results.OrderByDescending(p => p.name).ToList();
                    }
                    else if (sort == 3)
                    {
                        categoryModel.Results = categoryModel.Results.OrderBy(p => p.price_sell).ToList();
                    }
                    else
                    {
                        categoryModel.Results = categoryModel.Results.OrderByDescending(p => p.price_sell).ToList();
                    }
                }
                foreach (var item in categoryModel.Results)
                {
                    if (item.category_id.HasValue && item.category_id.Value > 0)
                    {
                        var category = _categoryService.GetByid(item.category_id.Value);
                        var productimages = _productsImagesService.GetImageProduct(item.id);
                        if (productimages != null && productimages.Count > 0)
                        {
                            var lstImages = _imagesService.GetImageName(productimages);
                            if (lstImages != null && lstImages.Count > 0)
                            {
                                item.avatar = lstImages[0].name;
                            }
                        }
                        if (category != null && !string.IsNullOrEmpty(category.name))
                        {
                            item.categorystr = category.name;
                        }
                        else
                        {
                            item.categorystr = "Chờ xử lý";
                        }
                    }
                    else
                    {
                        item.categorystr = "";
                    }
                    var productImage = _productsImagesService.GetImageProduct(item.id);
                    if (productImage != null && productImage.Count > 0)
                    {
                        var image = _imagesService.GetImageName(productImage);
                        if (image != null && image.Count > 0)
                        {
                            item.ImageModelView = image;
                        }
                    }
                    item.trademark = !string.IsNullOrEmpty(item.trademark) ? item.trademark : "";
                    item.price_sell_str = item.price_sell.HasValue && item.price_sell.Value > 0 ? item.price_sell.Value.ToString("#,###") : "";
                    item.price_import_str = item.price_import.HasValue && item.price_import.Value > 0 ? item.price_import.Value.ToString("#,###") : "";
                    item.price_reduced_str = item.price_reduced.HasValue && item.price_reduced.Value > 0 ? item.price_reduced.Value.ToString("#,###") : "";
                    model.Add(item);
                }
            }
            return View(model);
        }
        public IActionResult Index()
        {
            return View();
        }
       
    }
}