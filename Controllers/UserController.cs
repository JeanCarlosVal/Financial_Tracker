using Microsoft.AspNetCore.Mvc;
using Personal_Income.Models;
using System.Diagnostics;

namespace Personal_Income.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }
        
        public IActionResult Dashboard()
        {
            ViewBag.UserID = HttpContext.Session.GetInt32("SessionKeyID");
            ViewBag.UserName = HttpContext.Session.GetString("SessionKeyName");
            ViewBag.UserEmail = HttpContext.Session.GetString("SessionKeyEmail");
            return View();
        }

        public IActionResult New_Activity()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
