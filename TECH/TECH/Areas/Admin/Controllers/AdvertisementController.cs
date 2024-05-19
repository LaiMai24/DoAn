using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Service;

namespace TECH.Areas.Admin.Controllers
{
    public class AdvertisementController : BaseController
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public AdvertisementController(IAdvertisementService advertisementService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _advertisementService = advertisementService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            string data = _advertisementService.GetImage();
            var model = new AdvertisementModelView();
            if (!string.IsNullOrEmpty(data))
            {
                model.name = data;
            }
            return View(model);

        }

        public IActionResult UploadImageProduct()
        {
            var files = Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                var imageFolder = $@"\product-advertisement\";

                string folder = _hostingEnvironment.WebRootPath + imageFolder;

                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }              
                foreach (var itemFile in files)
                {
                    string fileNameFormat = Regex.Replace(itemFile.FileName.ToLower(), @"\s+", "");
                    string filePath = Path.Combine(folder, fileNameFormat);
                    if (!System.IO.File.Exists(filePath))
                    {                       
                        using (FileStream fs = System.IO.File.Create(filePath))
                        {
                            itemFile.CopyTo(fs);
                            fs.Flush();
                        }
                    }
                }
            }
            return Json(new
            {
                success = true
            });
        }

        [HttpPost]
        public JsonResult Add(string images)
        {
            _advertisementService.Add(images);
            _advertisementService.Save();
            return Json(new
            {
                success = true
            });

        }


    }
}
