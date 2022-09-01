using Microsoft.AspNetCore.Mvc;
using Personal_Income.Models;
using System.Diagnostics;
using Dapper;
using System.Data;
using System.Linq;

namespace Personal_Income.Controllers
{
    public class Sign_InController : Controller
    {
        public const string SessionKeyName = "_UserName";
        public const string SessionKeyID = "_ID";
        public const string SessionKeyEmail = "_Email";

        private readonly ILogger<Sign_InController> _logger;
        private readonly IConfiguration _configuration;
        public Sign_InController(ILogger<Sign_InController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Sign_In()
        {
            return View(new Sign_In_Model { Error = false });
        }

        public IActionResult Sign_Up()
        {
            return View(new Sign_Up_Model { IsPasswordConfirmed = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sign_In(Sign_In_Model user)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_configuration.GetConnectionString("CashFundDB")))
            {
                var output = connection.Query<Log_In_Session>("dbo.spLog_In @Email,@Password", new { Email = user.Email_Address, Password = user.Password }).ToList();
                if (!output.Count.Equals(0))
                {

                    HttpContext.Session.SetString("SessionKeyName", output[0].USERNAME);
                    HttpContext.Session.SetInt32("SessionKeyID", output[0].USERID);
                    HttpContext.Session.SetString("SessionKeyEmail", output[0].USEREMAIL);

                    return RedirectToAction("Dashboard","User");
                }
                else
                {
                    user.Error = true;
                    return View("Sign_In", user);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sign_Up(Sign_Up_Model user)
        {
            if (user.Password != user.PasswordConfirm)
            {
                user.IsPasswordConfirmed = false;

                return View(user);
            }

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(_configuration.GetConnectionString("CashFundDB")))
            {
                var output = connection.Query<ServerStatus>("dbo.spInsert_User @FirstName,@LastName,@Phone,@Email,@Password",
                    new { FirstName = user.UserName, LastName = user.UserLastName, Phone = user.UserPhoneNumber, Email = user.UserEmailAddress, Password = user.Password }).ToList();
                if (output[0].STATUS == -1)
                {
                    return View(user);
                }
                else
                {
                    return RedirectToAction("Sign_In", "Sign_In");
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