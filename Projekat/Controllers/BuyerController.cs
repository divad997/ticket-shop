using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Web.Security;

namespace Projekat.Controllers
{
    public class BuyerController : Controller
    {
        
        public ActionResult Index()
        {
            return View("~/Views/Buyer/LoggedInBuyer.cshtml");
        }
        public ActionResult MyProfile ()
        {
            ViewBag.user = Session["user"];
            return View("~/Views/Buyer/MyProfile.cshtml");
        }
        [HttpPost]
        public ActionResult EditProfile(string name, string surname, string password)
        {
            Database.ReadData();

            User user = (User)Session["user"];
            string username = user.Username;

            if (!name.Equals(""))
            {
                user.Name = name;
            }
            if (!surname.Equals(""))
            {
                user.Surname = surname;
            }
            if (!password.Equals(""))
            {
                user.Password = password;
            }

            foreach (User u in Database.users)
            {
                if (u.Username == user.Username)
                {
                    Database.users.Remove(u);
                    Database.users.Add(user);
                    Database.UpdateData();
                }
            }

            ViewBag.user = user;

            return View("~/Views/Buyer/MyProfile.cshtml");


        }

        public ActionResult BuyTicketView()
        {
            Database.ReadData();
            List<Show> allShows = Database.shows;
            ViewBag.shows = Database.shows;
            return View("~/Views/Buyer/BuyTicket.cshtml");
        }

        public ActionResult BuyFormView()
        {
            Database.ReadData();
            
            string id = Request["ID"];
            foreach (Show s in Database.shows)
            {
                if (s.Name.Equals(Request["ID"]))
                {
                    ViewBag.showBuy = s;

                }
            }
            return View("~/Views/Buyer/BuyTicketForm.cshtml");
        }

        public ActionResult Confirmation()
        {
            Database.ReadData();
            User u = (User)Session["user"];
            
            Show show = new Show();

            foreach (Show s in Database.shows)
            {
                if (s.Name.Equals(Request["ID"]))
                {
                    show = s;
                    ViewBag.showBuy = s;

                }
            }
            int number = Int32.Parse(Request["amount"]);
            double price;
            double raw_price;
            Enums.TicketType type = new Enums.TicketType();

            if (Request["type"].Equals("Regular"))
            {
                type = Enums.TicketType.Regular;

            }
            if (Request["type"].Equals("FanPit"))
            {
                type = Enums.TicketType.FanPit;

            }
            if (Request["type"].Equals("VIP"))
            {
                type = Enums.TicketType.VIP;

            }

            if (show.NumberOfSeats >= number)
            {
                if (u.Type.Discount > 0)
                {
                    ViewBag.Discount = u.Type.Discount;
                    if (type == Enums.TicketType.Regular)
                    {
                        raw_price = show.Price * number;
                        price = raw_price - (u.Type.Discount / 100 * raw_price);
                        ViewBag.price = price;
                    }
                    else if (type == Enums.TicketType.FanPit)
                    {
                        raw_price = show.Price * number * 2;
                        price = raw_price - (u.Type.Discount / 100 * raw_price);
                        ViewBag.price = price;
                    }
                    else if (type == Enums.TicketType.VIP)
                    {
                        raw_price = show.Price * number * 4;
                        price = raw_price - (u.Type.Discount / 100 * raw_price);
                        ViewBag.price = price;
                    }

                }
                else
                {
                    if (type == Enums.TicketType.Regular)
                    {
                        price = show.Price * number;
                        ViewBag.price = price;
                    }
                    else if (type == Enums.TicketType.FanPit)
                    {
                        price = show.Price * number * 2;
                        ViewBag.price = price;
                    }
                    else if (type == Enums.TicketType.VIP)
                    {
                        price = show.Price * number * 4;
                        ViewBag.price = price;
                    }


                }
            }
            else
            {
                ViewBag.showKupovina = show;
                ViewBag.seatnumber = show.NumberOfSeats;
                return View("~/Views/Buyer/NoTickets.cshtml");
            }

            ViewBag.type = type;
            ViewBag.ticketnumber = number;
            return View("~/Views/Buyer/Confirmation.cshtml");
        }

        public ActionResult Buy()
        {
            Database.ReadData();

            int amount = Int32.Parse(Request["ticketNumber"]);

            
            User u = (User)Session["user"];
            


            
            Enums.TicketType type = new Enums.TicketType();

            if (Request["type"].Equals("Regular"))
            {
                type = Enums.TicketType.Regular;

            }
            if (Request["type"].Equals("FanPit"))
            {
                type = Enums.TicketType.FanPit;

            }
            if (Request["type"].Equals("VIP"))
            {

                type = Enums.TicketType.VIP;
            }

            foreach (Show s in Database.shows)
            {
                if (Request["ID"].Equals(s.Name))
                {
                    int maxId = -1;
                    foreach (var item in Database.tickets)
                    {
                        if (item.ID > maxId)
                        {
                            maxId = item.ID;
                        }
                    }
                    
                    Ticket t = new Ticket(s, s.Start, float.Parse(Request["price"]), u.Username, Enums.TicketStatus.Reserved, s.Type, amount);
                    t.ID = maxId + 1;
                    s.NumberOfSeats -= amount;

                    Database.tickets.Add(t);
                    Database.UpdateData();
                    
                    if (type == Enums.TicketType.Regular)
                    {
                        u.Points = u.Points + (Int32.Parse(s.Price.ToString()) / 1000 * 133);
                        u.Type.ReqPoints = u.Type.ReqPoints - u.Points;


                    }
                    if (type == Enums.TicketType.FanPit)
                    {
                        u.Points = u.Points + ((Int32.Parse(s.Price.ToString()) * 2) / 1000 * 133);
                        u.Type.ReqPoints = u.Type.ReqPoints - u.Points;

                    }
                    if (type == Enums.TicketType.VIP)
                    {
                        u.Points = u.Points + ((Int32.Parse(s.Price.ToString()) * 4) / 1000 * 133);
                        u.Type.ReqPoints = u.Type.ReqPoints - u.Points;

                    }

                    int points = u.Points;
                    if (u.Points >= 1000)
                    {
                        u.Type.Name = "Bronze";
                        u.Type.Discount = 1;
                        u.Type.ReqPoints = 3000;

                    }
                    else if (u.Points >= 3000)
                    {
                        u.Type.Name = "Silver";
                        u.Type.Discount = 10;
                        u.Type.ReqPoints = 5000;
                    }
                    else if (u.Points >= 5000)
                    {
                        u.Type.Name = "Gold";
                        u.Type.Discount = 20;
                        u.Type.ReqPoints = 10000;
                    }

                    Database.users.Remove(Database.users.FirstOrDefault(x => x.Username == u.Username));
                    Database.users.Add(u);
                    Database.UpdateData();
                        
                }
            }
            return View("~/Views/Buyer/SuccessfulBuy.cshtml");
        }

        public ActionResult BoughtTicketsView()
        {
            Database.ReadData();
            User u = (User)Session["user"];
           

            ViewBag.tickets = Database.tickets;

            return View("~/Views/Buyer/ShowBoughtTickets.cshtml");
        }

        public ActionResult Search()
        {
            Database.ReadData();
            User u = (User)Session["user"];
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
        public ActionResult Sort()
        {
            User u = (User)Session["user"];

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

        public ActionResult Filter()
        {
            User u = (User)Session["user"];
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

        public ActionResult ReviewView()
        {
            User u = (User)Session["user"];
            ViewBag.u = u;
            
            ViewBag.show = Request["IDShow"];

            return View("~/Views/Buyer/Review.cshtml");
        }

        public ActionResult Review()
        {
            Database.ReadData();
            string show = Request["IDShow"];
            int rating = Int32.Parse(Request["rating"]);
            string comment = Request["comment"];
            Show show1 = new Show();

            foreach (Show s in Database.shows)
            {
                if (s.Name.Equals(Request["IDShow"]))
                {
                    show1 = s;
                }
            }

            User u = (User)Session["User"];

            

            Comment c = new Comment(u.Username, show1.Name, comment, rating);
            Database.commentshold.Add(c);



            return View("~/Views/Buyer/SuccessfulReview.cshtml");

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

            /*Response.ClearHeaders();
            Response.AddHeader("Cache-control", "no-cache, no-store, max-age=0,must-revalidate");
            Response.AddHeader("Pragma", "no-cache");*/


            return View("~/Views/Home/Index.cshtml");
        }
    }
}