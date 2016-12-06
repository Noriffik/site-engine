using System;
using Microsoft.AspNetCore.Mvc;

namespace ThinkingHome.SiteEngine.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("index<script src=\"js/bootstrap.js\"></script>", "text/html");
        }

        public ActionResult Info(string path)
        {
            return Content($"info: {path}");
        }
    }
}