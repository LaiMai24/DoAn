 using Microsoft.AspNetCore.Mvc;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Service;

namespace TECH.Areas.Admin.Controllers
{
    public class SizesController : BaseController
    {
        private readonly ISizesService _sizesService;
        public SizesController(ISizesService sizesService)
        {
            _sizesService = sizesService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetById(int id)
        {
            var model = new SizesModelView();
            if (id > 0)
            {
                model = _sizesService.GetByid(id);
            }
            return Json(new
            {
                Data = model
            });
        }

        [HttpGet]
        public JsonResult GetAll()
        {            
            var data = _sizesService.GetAll();
            return Json(new
            {
                Data = data
            });
        }

        [HttpGet]
        public IActionResult AddOrUpdate()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(SizesModelView SizesModelView)
        {

            bool isExist = false;
            if (SizesModelView != null && !string.IsNullOrEmpty(SizesModelView.name))
            {
                isExist = _sizesService.IsExist(SizesModelView.name);               
            }

            if (!isExist)
            {
                _sizesService.Add(SizesModelView);
                _sizesService.Save();
                return Json(new
                {
                    success = true
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    isCategoryNameExist = isExist
                });
            }
        }

        [HttpGet]
        public JsonResult UpdateStatus(int id,int status)
        {
            if (id > 0)
            {
               var  model = _sizesService.UpdateStatus(id, status);
                _sizesService.Save();
                return Json(new
                {
                    success = model
                });
            }
            return Json(new
            {
                success = false
            });

        }

        [HttpPost]
        public JsonResult Update(SizesModelView SizesModelView)
        {
            bool isCategoryNameExist = false;
            if (SizesModelView != null && !string.IsNullOrEmpty(SizesModelView.name))
            {
                isCategoryNameExist = _sizesService.IsExist(SizesModelView.name);
            }


            if (!isCategoryNameExist)
            {
                var result = _sizesService.Update(SizesModelView);
                _sizesService.Save();
                return Json(new
                {
                    success = result
                });
            }
            else
            {
                return Json(new
                {
                    success = false,
                    isCategoryNameExist = isCategoryNameExist
                });
            }


        }

     
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = _sizesService.Deleted(id);
            _sizesService.Save();
            return Json(new
            {
                success = result
            });
        }

        [HttpGet]
        public JsonResult GetAllPaging(CategoryViewModelSearch categoryViewModelSearch)
        {
            var data = _sizesService.GetAllPaging(categoryViewModelSearch);
            return Json(new { data = data });
        }
    }
}
