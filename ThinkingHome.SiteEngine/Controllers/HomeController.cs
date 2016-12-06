using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace ThinkingHome.SiteEngine.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;

        public HomeController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        public ActionResult Index()
        {
            return Content("index<script src=\"js/bootstrap.js\"></script>", "text/html");
        }

        public ActionResult Info(string path)
        {
            var serverPath = Path.Combine(_appEnvironment.ContentRootPath, "content", path);
            var content = System.IO.File.ReadAllText(serverPath);
            return Content(CommonMark.CommonMarkConverter.Convert(content), "text/html");
        }
    }
}