using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekat.Controllers
{
    public class UlogujSeController : Controller
    {
        // GET: UlogujSe
        public ActionResult Index()
        {
            return View("~/Views/HomePage/UlogujSe.cshtml");
        }
        public ActionResult UlogujSe()
        {
            string name = Request["Username"];
            //List<User> users = Server.ListaKorisnika();
            string pass = Request["Password"];
            Database.ReadData();



            foreach (User u in Database.users)
            {


                if ((name.Equals(u.Username)) && pass.Equals(u.Password))
                {

                    if (u.Role == Enums.Role.Admin)
                    {
                        Session["user"] = u;

                        return View("~/Views/Admin/LoggedInAdmin.cshtml");


                    }
                    if (u.Role == Enums.Role.Seller)
                    {

                        Session["user"] = u;
                        return View("~/Views/Seller/LoggedInSeller.cshtml");


                    }
                    else
                    {

                        Session["user"] = u;
                        return View("~/Views/Buyer/LoggedInBuyer.cshtml");


                    }
                }


            }

            return View("~/Views/Home/UserNoExist.cshtml");


        }

    }
}