 using Microsoft.AspNetCore.Mvc;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Service;

namespace TECH.Areas.Admin.Controllers
{
    public class ColorsController : BaseController
    {
        private readonly IColorsService _colorsService;
        public ColorsController(IColorsService colorsService)
        {
            _colorsService = colorsService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetById(int id)
        {
            var model = new ColorsModelView();
            if (id > 0)
            {
                model = _colorsService.GetByid(id);
            }
            return Json(new
            {
                Data = model
            });
        }

        [HttpGet]
        public JsonResult GetAll()
        {            
            var data = _colorsService.GetAll();
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
        public JsonResult Add(ColorsModelView ColorsModelView)
        {

            bool isExist = false;
            if (ColorsModelView != null && !string.IsNullOrEmpty(ColorsModelView.name))
            {
                isExist = _colorsService.IsExist(ColorsModelView.name);               
            }

            if (!isExist)
            {
                _colorsService.Add(ColorsModelView);
                _colorsService.Save();
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
               var  model = _colorsService.UpdateStatus(id, status);
                _colorsService.Save();
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
        public JsonResult Update(ColorsModelView ColorsModelView)
        {
            bool isCategoryNameExist = false;
            if (ColorsModelView != null && !string.IsNullOrEmpty(ColorsModelView.name))
            {
                isCategoryNameExist = _colorsService.IsExist(ColorsModelView.name);
            }


            if (!isCategoryNameExist)
            {
                var result = _colorsService.Update(ColorsModelView);
                _colorsService.Save();
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
            var result = _colorsService.Deleted(id);
            _colorsService.Save();
            return Json(new
            {
                success = result
            });
        }

        [HttpGet]
        public JsonResult GetAllPaging(CategoryViewModelSearch categoryViewModelSearch)
        {
            var data = _colorsService.GetAllPaging(categoryViewModelSearch);
            return Json(new { data = data });
        }
    }
}
