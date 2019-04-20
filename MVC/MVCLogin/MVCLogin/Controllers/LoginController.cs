using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCLogin.Models;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(MVCLogin.Models.User userModel)
        {
            using (LoginDatabaseEntities db = new LoginDatabaseEntities())
            {
                var userDetails = db.Users.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Invalid username or password";
                    return View("Index",userModel);
                }
                else
                {
                    Session["userId"] = userDetails.UserId;
                    Session["username"] = userDetails.Username;
                    return RedirectToAction("Index", "Home");
                }
            }
           
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}