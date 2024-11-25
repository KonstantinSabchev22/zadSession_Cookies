using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using zadSession_Cookies.Models;

namespace zadSession_Cookies.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //»звличане на UserPreference от бисквитката
            if (Request.Cookies.TryGetValue("UserPreference", out string userPreference))
            {
                ViewBag.UserPreference = userPreference;
            }
            else
            {
                ViewBag.UserPreference = "Default"; // —тойност по подразбиране, ако н€ма зададена бисквитка
            }
            ViewBag.UserName = HttpContext.Session.GetString("UserName"); // ¬земане на името от сеси€та, ако е налично
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //Storing the user name in the session
        [HttpPost]

        public IActionResult SetUserName(string userName)
        {
            HttpContext.Session.SetString("UserName", userName);
            return RedirectToAction("Index");
        }


        public IActionResult SetCookie(string preference) 
        {
            Response.Cookies.Append("Userpreference", preference, new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(7),
                HttpOnly = true,
                IsEssential = true
            });
            return RedirectToAction("Index");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
