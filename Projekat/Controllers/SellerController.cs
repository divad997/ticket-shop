using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Projekat.Controllers
{
    public class SellerController : Controller
    {
        // GET: Seller
        public ActionResult Index()
        {
            return View("~/Views/Seller/LoggedInSeller.cshtml");
        }
        public ActionResult MyProfile()
        {
            ViewBag.korisnik = Session["user"];
            return View("~/Views/Seller/MyProfile.cshtml");
        }
        [HttpPost]
        public ActionResult PromeniPodatke(string name, string surname, string password)
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

            return View("~/Views/Seller/MyProfile.cshtml");
        }

        public ActionResult AddShow()
        {
            return View("~/Views/Seller/AddShow.cshtml");
        }
        public ActionResult NovaShow()
        {
            Database.ReadData();

            Enums.ShowType type = new Enums.ShowType();
            

            string filename = "";

            


            if (Request["type"].Equals(Enums.ShowType.Festival))
            {
                type = Enums.ShowType.Festival;
            }
            if (Request["type"].Equals(Enums.ShowType.Concert))
            {
                type = Enums.ShowType.Concert;
            }
            if (Request["type"].Equals(Enums.ShowType.Cinema))
            {
                type = Enums.ShowType.Cinema;
            }
            if (Request["type"].Equals(Enums.ShowType.Theatre))
            {
                type = Enums.ShowType.Theatre;
            }

            Address ad = new Address(Request["city"], Request["street"], Int32.Parse(Request["zipCode"]));
            

            foreach (Show s in Database.shows)
            {
                if ((s.Address.Street.Equals(Request["street"])) && (s.Start.Equals(Request["date"])) && (s.Duration.Equals(Request["duration"])))
                {
                    return View("~/Views/Seller/ShowExist.cshtml");
                }
            }
            Show show = new Show(Request["naziv"], type, Int32.Parse(Request["brojMesta"]), DateTime.Parse(Request["date"]), float.Parse(Request["price"]), Enums.ShowStatus.Inactive, ad, filename);
            Database.showshold.Add(show);

            
            return View("~/Views/Seller/LoggedInSeller.cshtml");
        }

        public ActionResult EditShow()
        {
            List<Show> shows = Database.shows;
            ViewBag.shows = shows;
            return View("~/Views/Seller/Manifestacije.cshtml");
        }
        public ActionResult EditShowData()
        {
            List<Show> shows = Database.shows;
            foreach (Show s in shows)
            {
                if (s.Name.Equals(Request["ID"]))
                {
                    ViewBag.show = s;
                }
            }
            return View("~/Views/Seller/EditShow.cshtml");
        }

        [HttpPost]
        public ActionResult Edit(string name, string number, string date, string duration, string city, string street, string zipcode, string price)
        {
            Database.ReadData();

            Show show = new Show();
           

            foreach (Show s in Database.shows)
            {
                if (s.Name.Equals(name))
                {
                    show = s;
                    Database.shows.Remove(s);
                }
            }


            if (!name.Equals(""))
            {
                show.Name = name;
            }
            if (!number.Equals(""))
            {
                show.NumberOfSeats = Int32.Parse(number);
            }
            if (!date.Equals(""))
            {
                show.Start = DateTime.Parse(date);
            }
            if (!duration.Equals(""))
            {
                show.Duration = TimeSpan.Parse(duration);
            }
            if (!city.Equals(""))
            {
                show.Address.City = city;
            }
            if (!street.Equals(""))
            {
                show.Address.Street = street;
            }       
            if (!zipcode.Equals(""))
            {
                show.Address.ZipCode = Int32.Parse(zipcode);
            }
            if (!price.Equals(""))
            {
                show.Price = Int32.Parse(price);
            }

            Database.shows.Add(show);
            Database.UpdateData();
            


            ViewBag.show = show;

            return View("~/Views/Seller/EditShow.cshtml");
        }
        public ActionResult ShowTicketsView()
        {
            Database.ReadData();
            ViewBag.tickets = Database.tickets;
            return View("~/Views/Seller/ShowTickets.cshtml");
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
            ViewBag.tickets = searchedTickets;
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
            else if (Request["sortBy"].Equals("dateITime"))
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
            ViewBag.tickets = sortedTickets;
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

            ViewBag.tickets = filteredTickets;
            return View("~/Views/Buyer/ShowBoughtTickets.cshtml");
        }

        public ActionResult ApproveCommentView()
        {
            Database.ReadData();
            List<Comment> commentsHold = Database.commentshold;
            ViewBag.comments = commentsHold;
            return View("~/Views/Seller/ApproveComment.cshtml");
        }
        public ActionResult Approve()
        {
            Database.ReadData();
            string comment = Request["IDComment"];
            string buyer = Request["IDBuyer"];
            string show = Request["IDShow"];
            Show show1 = new Show();

            foreach (Show s in Database.shows)
            {
                if (s.Name.Equals(show))
                {
                    show1 = s;
                    Database.shows.Remove(s);
                }
            }

            foreach (Comment c in Database.commentshold)
            {
                if ((c.Post.Equals(comment)) && (c.Buyer.Equals(buyer)))
                {


                    Database.comments.Add(c);
                    Database.commentshold.Remove(c);
                    show1.CalculateRating();

                    Database.shows.Add(show1);
                    Database.UpdateData();
                
                    

                }
            }


            ViewBag.comments = Database.commentshold;
            if (Database.commentshold.Count == 0)
            {
                return View("~/Views/Seller/NoCommentsOnHold.cshtml");
            }
            else
            {
                return View("~/Views/Seller/ApproveComment.cshtml");
            }

        }
        public ActionResult Reject()
        {
            Database.ReadData();
            string comment = Request["IDComment"];
            string buyer = Request["IDBuyer"];
            string show = Request["IDShow"];
            Show show1 = new Show();

            foreach (Show s in Database.shows)
            {
                if (s.Name.Equals(show))
                {
                    show1 = s;
                }
            }

            foreach (Comment c in Database.commentshold)
            {
                if ((c.Post.Equals(comment)) && (c.Buyer.Equals(buyer)))
                {

                    Database.commentsrejected.Add(c);
                    Database.commentshold.Remove(c);
                    Database.UpdateData();


                }
            }
            ViewBag.comments = Database.commentshold;

            if (Database.commentshold.Count == 0)
            {
                return View("~/Views/Seller/NoCommentsOnHold.cshtml");
            }
            else
            {
                return View("~/Views/Seller/ApproveComment.cshtml");
            };


        }

        public ActionResult AllComments()
        {
            Database.ReadData();
            ViewBag.comments = Database.comments;
            ViewBag.commentsRejected = Database.commentsrejected;

            return View("~/Views/Seller/AllComments.cshtml");
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