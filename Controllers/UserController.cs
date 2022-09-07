using Dapper;
using Microsoft.AspNetCore.Mvc;
using Personal_Income.Models;
using System.Data;
using System.Diagnostics;

namespace Personal_Income.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;

        public UserController(ILogger<UserController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
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
            return View(new RecordModel { Error = false, Success = false});
        }

        public IActionResult Profile()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New_Activity(RecordModel record)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_configuration.GetConnectionString("CashFundDB")))
            {
                var output = connection.Query<ServerStatus>("dbo.spInsertRecord @ID,@Name,@Type,@Cost,@Date", 
                    new {ID = ViewBag.UserID, Name = record.ActivityName, Type = record.ActivityType, Cost = record.ActivityCost, Date = record.ActivityDate}).ToList();

                if (output[0].STATUS == -1)
                {
                    return View(new RecordModel { Error = true });
                }
                else
                {
                    return View(new RecordModel { Success = true});
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
