using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestApp.Models;

namespace TestApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController()
        {
           
        }

        public IActionResult Index(string userName = "Unknown")
        {
            var existingUserName = HttpContext.Session.GetString("UserName");
            if(existingUserName != null && existingUserName != "Unknown")
            {
                return View();
            }

            HttpContext.Session.SetString("UserName", userName);
            ViewData["UserName"] = userName;
            if(userName == "Unknown")
            {
                return View("landing");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            var userName = HttpContext.Session.GetString("UserName");

            ViewData["UserName"] = userName;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
