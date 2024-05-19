using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using TECH.Areas.Admin.Models;
using TECH.Areas.Admin.Models.Search;
using TECH.Service;

namespace TECH.Areas.Admin.Controllers
{
    public class SidersController : BaseController
    {
        private readonly ISidersService _sidersService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        public SidersController(ISidersService sidersService,
            Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _sidersService = sidersService;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            var data = _sidersService.GetImage();
            return View(data);
        }


        [HttpPost]
        public JsonResult Delete(int id)
        {
            var result = _sidersService.Deleted(id);
            _sidersService.Save();
            return Json(new
            {
                success = result
            });
        }


        public IActionResult UploadImageProduct()
        {
            var files = Request.Form.Files;
            if (files != null && files.Count > 0)
            {
                var imageFolder = $@"\product-siders\";

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
            _sidersService.Add(images);
            _sidersService.Save();
            return Json(new
            {
                success = true
            });

        }


    }
}
