using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Projekat.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View("~/Views/Admin/LoggedInAdmin.cshtml");
        }
        public ActionResult MyProfile()
        {
            ViewBag.user = Session["user"];
            return View("~/Views/Admin/MyProfile.cshtml");
        }
        [HttpPost]
        public ActionResult EditProfile(string name, string lastname, string password)
        {
            Database.ReadData();
            

            User user = (User)Session["user"];
            string username = user.Username;

            if (!name.Equals(""))
            {
                user.Name = name;
            }
            if (!lastname.Equals(""))
            {
                user.Surname = lastname;
            }
            if (!password.Equals(""))
            {
                user.Password = password;
            }
            
            foreach (User u in Database.users)
            {
                if (u.Username == username)
                {
                    Database.users.Remove(u);
                    Database.users.Add(user);
                    Database.UpdateData();
                }
            }
           


            ViewBag.user = user;

            return View("~/Views/Admin/MyProfile.cshtml");

        }

        public ActionResult AddSeller()
        {
            return View("~/Views/Administrator/AddSeller.cshtml");

        }
        public ActionResult CreateSeller()
        {

            Database.ReadData();

          
            if (Database.UserExists(Request["username"]))
            {
                return View("~/Views/Home/UserExist.cshtml");
            }
            else
            {
                User us = new User(Request["username"], Request["password"], Request["name"], Request["lastname"], DateTime.Parse(Request["date"]), Enums.Role.Seller, new List<Ticket>(), new List<Show>(), 0, new UserType("Bronze", 0, 10));

                Database.users.Add(us);
                Database.UpdateData();

                Session["user"] = us;




                return View("~/Views/Admin/SellerCreated.cshtml");
            }
        }

        public ActionResult ShowUsers()
        {
            ViewBag.korisnici = Database.users;
            return View("~/Views/Admin/ShowUsers.cshtml");
        }

        public ActionResult Search()
        {
            Database.ReadData();
            List<User> searchedUsers = new List<User>();
            foreach (User u in Database.users)
            {
                if ((Request["search_value"].ToUpper().Equals(u.Name.ToUpper())) || (Request["search_value"].ToUpper().Equals(u.Surname.ToUpper())) || (Request["search_value"].ToUpper().Equals(u.Username.ToUpper())))
                {
                    searchedUsers.Add(u);
                }
            }

            ViewBag.users = searchedUsers;
            return View("~/Views/Admin/ShowUsers.cshtml");
        }
        public ActionResult Sort()
        {
            Database.ReadData();
            List<User> sortedUsers = new List<User>();
            List<User> allUsers = Database.users;
            

            if (Request["sortType"].Equals("ascending"))
            {

                if (Request["sortBy"].Equals("name"))
                {
                    sortedUsers = allUsers.OrderBy(o => o.Name).ToList();
                }
                else if (Request["sortBy"].Equals("surname"))
                {
                    sortedUsers = allUsers.OrderBy(o => o.Surname).ToList();
                }
                else if (Request["sortBy"].Equals("username"))
                {
                    sortedUsers = allUsers.OrderBy(o => o.Username).ToList();
                }
                else if (Request["sortBy"].Equals("points"))
                {
                    sortedUsers = allUsers.OrderBy(o => o.Points).ToList();
                }


            }
            else if (Request["sortType"].Equals("descending"))
            {


                if (Request["sortBy"].Equals("name"))
                {
                    sortedUsers = allUsers.OrderByDescending(o => o.Name).ToList();
                }
                else if (Request["sortBy"].Equals("surname"))
                {
                    sortedUsers = allUsers.OrderByDescending(o => o.Surname).ToList();
                }
                else if (Request["sortBy"].Equals("username"))
                {
                    sortedUsers = allUsers.OrderByDescending(o => o.Username).ToList();
                }
                else if (Request["sortBy"].Equals("points"))
                {
                    sortedUsers = allUsers.OrderByDescending(o => o.Points).ToList();
                }


            }
            ViewBag.users = sortedUsers.ToList();
            return View("~/Views/Administrator/ShowUsers.cshtml");
        }
        public ActionResult Filter()
        {
            Database.ReadData();
            List<User> allUsers = Database.users;
            List<User> filteredUsers = new List<User>();

            if ((!Request["role"].Equals("")) && (!Request["type"].Equals("")))
            {
                string role = Request["role"];
                string type = Request["type"];

                foreach (User u in allUsers)
                {
                    if ((u.Role.ToString().Equals(role)) && (u.Type.Name.Equals(type)))
                    {
                        filteredUsers.Add(u);
                    }
                }
            }

            if ((!Request["role"].Equals("")) && (Request["type"].Equals("")))
            {
                if (Request["role"].Equals("Buyer"))
                {
                    foreach (User u in allUsers)
                    {
                        if (u.Role.Equals(Enums.Role.Buyer))
                        {
                            if (!filteredUsers.Contains(u))
                            {
                                filteredUsers.Add(u);
                            }
                        }
                    }
                }
                else if (Request["role"].Equals("Seller"))
                {
                    foreach (User u in allUsers)
                    {
                        if (u.Role.Equals(Enums.Role.Seller))
                        {
                            if (!filteredUsers.Contains(u))
                            {
                                filteredUsers.Add(u);
                            }
                        }
                    }

                }
                else if (Request["role"].Equals("Admin"))
                {
                    foreach (User u in allUsers)
                    {
                        if (u.Role.Equals(Enums.Role.Admin))
                        {
                            if (!filteredUsers.Contains(u))
                            {
                                filteredUsers.Add(u);
                            }
                        }
                    }

                }
            }
            if ((!Request["type"].Equals("")) && (Request["role"].Equals("")))
            {
                if (Request["type"].Equals("Bronze"))
                {
                    foreach (User u in allUsers)
                    {
                        if (u.Type.Name.Equals("Bronze"))
                        {
                            if (!filteredUsers.Contains(u))
                            {
                                filteredUsers.Add(u);
                            }
                        }
                    }
                }
                else if (Request["type"].Equals("Silver"))
                {
                    foreach (User u in allUsers)
                    {
                        if (u.Type.Name.Equals("Silver"))
                        {
                            if (!filteredUsers.Contains(u))
                            {
                                filteredUsers.Add(u);
                            }
                        }
                    }
                }
                else if (Request["type"].Equals("Gold"))
                {
                    foreach (User u in allUsers)
                    {
                        if (u.Type.Name.Equals("Gold"))
                        {
                            if (!filteredUsers.Contains(u))
                            {
                                filteredUsers.Add(u);
                            }

                        }
                    }
                }

            }
            ViewBag.korisnici = filteredUsers;
            return View("~/Views/Admin/ShowUsers.cshtml");

        }
        public ActionResult ShowsOnHold()
        {
            Database.ReadData();

            ViewBag.showsOnHold = Database.showshold;
            return View("~/Views/Admin/ShowsOnHold.cshtml");

        }
        public ActionResult Reject()
        {
            Database.ReadData();

            foreach (Show s in Database.showshold)
            {
                if (s.Name.Equals(Request["ID"]))
                {
                    Database.showshold.Remove(s);

                    Database.UpdateData();
                }      
            }

            ViewBag.showOnHold = Database.showshold;
            return View("~/Views/Admin/ShowsOnHold.cshtml");
        }
        public ActionResult Approve()
        {
            Database.ReadData();

      
            foreach (Show s in Database.showshold)
            {
                if (s.Name.Equals(Request["ID"]))
                {
                    s.Status = Enums.ShowStatus.Active;

                    Database.shows.Add(s);
                    Database.showshold.Remove(s);
                    Database.UpdateData();

                }
            }

            ViewBag.showsOnHold = Database.showshold;


            return View("~/Views/Admin/ShowsOnHold.cshtml");
        }

        public ActionResult ShowTicketView()
        {
            Database.ReadData();
            ViewBag.tickets = Database.tickets;
            return View("~/Views/Admin/ShowTickets.cshtml");
        }
        public ActionResult SearchTickets()
        {
            Database.ReadData();
            List<Ticket> searchedTickets = new List<Ticket>();

            foreach (Ticket t in Database.tickets)
            {
                DateTime date = t.Time;
                DateTime dateFrom = DateTime.Parse(Request["dateFrom"]);
                DateTime dateTo = DateTime.Parse(Request["dateTo"]);

                int compareFrom = DateTime.Compare(date, dateFrom); 
                int compareTo = DateTime.Compare(date, dateTo); 


                int priceFrom = Int32.Parse(Request["priceFrom"]);
                int priceTo = Int32.Parse(Request["priceTo"]);

                if ((compareFrom >= 0) && (compareTo <= 0) && (t.TotalPrice >= priceFrom) && (t.TotalPrice <= priceTo))
                {
                    if (t.Show.Name.ToUpper().Equals(Request["search_value"].ToUpper()))
                    {

                        if (!searchedTickets.Contains(t))
                        {
                            searchedTickets.Add(t);
                        }
                    }

                }

            }
            ViewBag.karte = searchedTickets;
            return View("~/Views/Buyer/ShowBoughtTickets.cshtml");
        }
        public ActionResult SortTickets()
        {
            Database.ReadData();
            List<Ticket> allTickets = Database.tickets;
            List<Ticket> sortedTickets = new List<Ticket>();

            if (Request["sortBy"].Equals("name"))
            {
                if (Request["sortType"].Equals("ascending"))
                {
                    sortedTickets = allTickets.OrderBy(o => o.Show.Name).ToList();
                }
                else if (Request["sortType"].Equals("descending"))
                {
                    sortedTickets = allTickets.OrderByDescending(o => o.Show.Name).ToList();
                }
            }
            else if (Request["sortBy"].Equals("dateITName"))
            {
                if (Request["sortType"].Equals("ascending"))
                {
                    sortedTickets = allTickets.OrderBy(o => o.Time).ToList();

                }
                else if (Request["sortType"].Equals("descending"))
                {
                    sortedTickets = allTickets.OrderByDescending(o => o.Time).ToList();

                }
            }
            else if (Request["sortBy"].Equals("price"))
            {
                if (Request["sortType"].Equals("ascending"))
                {
                    sortedTickets = allTickets.OrderBy(o => o.TotalPrice).ToList();
                }
                else if (Request["sortType"].Equals("descending"))
                {
                    sortedTickets = allTickets.OrderByDescending(o => o.TotalPrice).ToList();
                }
            }
            ViewBag.karte = sortedTickets;
            return View("~/Views/Buyer/ShowBoughtTickets.cshtml");
        }

        public ActionResult FilterTickets()
        {
            Database.ReadData();
            List<Ticket> filteredTickets = new List<Ticket>();

            if ((!Request["type"].Equals("")) && (!Request["status"].Equals("")))
            {
                if (Request["status"].Equals("active"))
                {
                    if (Request["type"].Equals("Regular"))
                    {
                        foreach (Ticket t in Database.tickets)
                        {
                            if ((t.Status == Enums.TicketStatus.Reserved) && (t.TicketType == Enums.TicketType.Regular))
                            {
                                filteredTickets.Add(t);
                            }
                        }
                    }
                    else if (Request["type"].Equals("FanPit"))
                    {
                        foreach (Ticket t in Database.tickets)
                        {
                            if ((t.Status == Enums.TicketStatus.Reserved) && (t.TicketType == Enums.TicketType.FanPit))
                            {
                                filteredTickets.Add(t);
                            }
                        }

                    }
                    else if (Request["type"].Equals("VIP"))
                    {
                        foreach (Ticket t in Database.tickets)
                        {
                            if ((t.Status == Enums.TicketStatus.Reserved) && (t.TicketType == Enums.TicketType.VIP))
                            {
                                filteredTickets.Add(t);
                            }
                        }

                    }
                }
                else if (Request["status"].Equals("inactive"))
                {
                    if (Request["type"].Equals("Regular"))
                    {
                        foreach (Ticket t in Database.tickets)
                        {
                            if ((t.Status == Enums.TicketStatus.Cancelled) && (t.TicketType == Enums.TicketType.Regular))
                            {
                                filteredTickets.Add(t);
                            }
                        }
                    }
                    else if (Request["type"].Equals("FanPit"))
                    {
                        foreach (Ticket t in Database.tickets)
                        {
                            if ((t.Status == Enums.TicketStatus.Cancelled) && (t.TicketType == Enums.TicketType.FanPit))
                            {
                                filteredTickets.Add(t);
                            }
                        }

                    }
                    else if (Request["type"].Equals("VIP"))
                    {
                        foreach (Ticket t in Database.tickets)
                        {
                            if ((t.Status == Enums.TicketStatus.Cancelled) && (t.TicketType == Enums.TicketType.Regular))
                            {
                                filteredTickets.Add(t);
                            }
                        }

                    }

                }

            }
            else if ((!Request["type"].Equals("")) && (Request["status"].Equals("")))
            {
                if (Request["type"].Equals("Regular"))
                {
                    foreach (Ticket t in Database.tickets)
                    {
                        if (t.TicketType == Enums.TicketType.Regular)
                        {
                            filteredTickets.Add(t);
                        }
                    }
                }
                else if (Request["type"].Equals("FanPit"))
                {
                    foreach (Ticket t in Database.tickets)
                    {
                        if (t.TicketType == Enums.TicketType.FanPit)
                        {
                            filteredTickets.Add(t);
                        }
                    }

                }
                else if (Request["type"].Equals("VIP"))
                {
                    foreach (Ticket t in Database.tickets)
                    {
                        if (t.TicketType == Enums.TicketType.VIP)
                        {
                            filteredTickets.Add(t);
                        }
                    }

                }

            }
            else if ((Request["type"].Equals("")) && (!Request["status"].Equals("")))
            {
                if (Request["status"].Equals("active"))
                {
                    foreach (Ticket t in Database.tickets)
                    {
                        if (t.Status == Enums.TicketStatus.Reserved)
                        {
                            filteredTickets.Add(t);
                        }
                    }
                }
                else if (Request["status"].Equals("inactive"))
                {
                    foreach (Ticket t in Database.tickets)
                    {
                        if (t.Status == Enums.TicketStatus.Cancelled)
                        {
                            filteredTickets.Add(t);
                        }
                    }
                }
            }

            ViewBag.karte = filteredTickets;
            return View("~/Views/Buyer/ShowBoughtTickets.cshtml");
        }

        public ActionResult ShowComments()
        {
            Database.ReadData();
            ViewBag.comments = Database.comments;
            ViewBag.commentsRejected = Database.commentsrejected;

            return View("~/Views/Admin/AllComments.cshtml");
        }

        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            FormsAuthentication.SignOut();


            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();

            return View("~/Views/Home/Index.cshtml");
        }
    }
}