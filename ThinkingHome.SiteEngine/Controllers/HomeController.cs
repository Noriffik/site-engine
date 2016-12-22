using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ThinkingHome.SiteEngine.Helpers;

namespace ThinkingHome.SiteEngine.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;

        public HomeController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        [ResponseCache(Duration = 60)]
        public ActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 60)]
        public ActionResult Md(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "readme.md";
            }

            var serverPath = Path.Combine(_appEnvironment.ContentRootPath, "content", path);
            var md = System.IO.File.ReadAllText(serverPath);
            var content = Markdown.ConvertToHtml(md);
            return View((object)content);
        }
    }
}