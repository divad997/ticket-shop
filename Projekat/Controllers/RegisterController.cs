using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projekat.Models;

namespace Projekat.Controllers
{
    public class RegisterController : Controller
    {

        

        // GET: RegistrujSe
        public ActionResult Index()
        {
            return View("~/Views/Home/Register.cshtml");
        }
        public ActionResult Register()
        {

            Database.ReadData();

            if (Database.UserExists(Request["username"]))
            {
                return View("~/Views/Homepage/UserExist.cshtml");
            }
            else
            {
                User us = new User(Request["username"], Request["password"], Request["name"], Request["lastname"], DateTime.Parse(Request["date"]), Enums.Role.Buyer, 0, new UserType("Bronze", 0, 10));

                Database.users.Add(us);
                Database.UpdateData();

                Session["user"] = us;
                
                
                

                return View("~/Views/Buyer/LoggedInBuyer.cshtml");
            }

        }

        public ActionResult MyProfile()
        {
            ViewBag.user = Session["user"];
            return View("~/Views/MyProfile.cshtml");
        }
    }
}