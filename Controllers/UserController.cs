using Dapper;
using Microsoft.AspNetCore.Mvc;
using Personal_Income.Models;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

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
            var session = HttpContext.Session.GetString("session");
            var userSession = JsonSerializer.Deserialize<List<Log_In_Session>>(session!);

            ViewBag.UserName = userSession?[0].USERNAME;

            return View();
        }

        public IActionResult New_Activity()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_configuration.GetConnectionString("CashFundDB")))
            {
                var Id = "";

                var output = connection.Query<ActivityList>("dbo.spGetActivityType @ID",
                    new { ID = Id }).ToList();
            }
                return View(new RecordModel { Error = false, Success = false });
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
                var Id = (int)HttpContext.Session.GetInt32("SessionKeyID")!;
                var date = record.ActivityDate!.Replace("T", " ");
                var cost = Decimal.Parse(record.ActivityCost!);

                var output = connection.Query<ServerStatus>("dbo.spInsertRecord @ID,@Name,@Type,@Cost,@Date", 
                    new {ID = Id, Name = record.ActivityName, Type = record.ActivityType, Cost = cost, Date = date}).ToList();

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
