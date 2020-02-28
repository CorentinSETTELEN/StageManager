using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using StageManager.DAL;
using StageManager.Models;

namespace StageManager.Controllers
{
    public class HomeController : Controller
    {
        private StageManagerContext db = new StageManagerContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(User userInput)
        {
            var userLogin = db.Users.SingleOrDefault(user => user.Username == userInput.Username && user.Password == userInput.Password);

            if (userLogin == null)
            {
                ViewBag.errorLogin = "Username or password are incorrects";
                return View();
            }

            else
            {
                Session["username"] = userLogin.Username;
                Session["role"] = userLogin.Role.ToString();

                if (userLogin.Role.ToString() == "stagiaire")
                {
                    var stagiaireFromUser = db.Stagiaires.SingleOrDefault(stagiaire => stagiaire.User.ID == userLogin.ID);

                    Session["id"] = stagiaireFromUser.ID;

                }
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}