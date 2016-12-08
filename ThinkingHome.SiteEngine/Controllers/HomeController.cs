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

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Md(string path)
        {
            var serverPath = Path.Combine(_appEnvironment.ContentRootPath, "content", path);
            var md = System.IO.File.ReadAllText(serverPath);
            var content = MarkdownHelper.ConvertToHtml(md);
            return View((object)content);
        }
    }
}