using Projekat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projekat.Controllers
{
    public class ShowController : Controller
    {
        // GET: Show
        public ActionResult Index()
        {
            Database.ReadData();
            List<Show> searchedShows = Database.shows;
            searchedShows.OrderBy(o => o.Start);
            ViewBag.shows = searchedShows;
            return View("~/Views/Shows/ShowShows.cshtml");
        }
        public ActionResult Search()
        {
            List<Show> searchedShows = new List<Show>();

            foreach (Show s in Database.shows)
            {
                DateTime date_shows = s.Start;
                DateTime dateFrom = DateTime.Parse(Request["dateFrom"]);
                DateTime dateTo = DateTime.Parse(Request["dateTo"]);

                int poredjenje_dateaFrom = DateTime.Compare(date_shows, dateFrom); //TREBA DA BUDE 0 ILI 1
                int poredjenje_dateaTo = DateTime.Compare(date_shows, dateTo); //TREBA DA BUDE 0 ILI -1


                int priceFrom = Int32.Parse(Request["priceFrom"]);
                int priceTo = Int32.Parse(Request["priceTo"]);

                if ((poredjenje_dateaFrom >= 0) && (poredjenje_dateaTo <= 0) && (s.Price >= priceFrom) && (s.Price <= priceTo))
                {
                    if ((s.Name.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.City.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.Street.ToUpper().Equals(Request["search_value"].ToUpper())))
                    {
                        if (!searchedShows.Contains(s))
                        {
                            searchedShows.Add(s);
                        }
                    }

                }

            }
            ViewBag.shows = searchedShows;
            return View("~/Views/Shows/ShowShows.cshtml");
        }

       
        public ActionResult Sort()
        {
            List<Show> shows = Database.shows;
            List<Show> sortedShows = new List<Show>();

            if (Request["sortBy"].Equals("naziv"))
            {
                if (Request["sortType"].Equals("ascending"))
                {
                    sortedShows = shows.OrderBy(o => o.Name).ToList();
                }
                else if (Request["sortType"].Equals("descending"))
                {
                    sortedShows = shows.OrderByDescending(o => o.Name).ToList();
                }
            }
            else if (Request["sortBy"].Equals("dateIVreme"))
            {
                if (Request["sortType"].Equals("ascending"))
                {
                    sortedShows = shows.OrderBy(o => o.Start).ToList();

                }
                else if (Request["sortType"].Equals("descending"))
                {
                    sortedShows = shows.OrderByDescending(o => o.Start).ToList();

                }
            }
            else if (Request["sortBy"].Equals("price"))
            {
                if (Request["sortType"].Equals("ascending"))
                {
                    sortedShows = shows.OrderBy(o => o.Price).ToList();
                }
                else if (Request["sortType"].Equals("descending"))
                {
                    sortedShows = shows.OrderByDescending(o => o.Price).ToList();
                }
            }
            else if (Request["sortBy"].Equals("mestoFromrzavanja"))
            {
                if (Request["sortType"].Equals("ascending"))
                {
                    sortedShows = shows.OrderBy(o => o.Address.City).ToList();
                }
                else if (Request["sortType"].Equals("descending"))
                {
                    sortedShows = shows.OrderByDescending(o => o.Address.City).ToList();
                }
            }

            ViewBag.shows = sortedShows;
            return View("~/Views/Shows/ShowShows.cshtml");
        }

        public ActionResult Filtriraj()
        {
            List<Show> shows = Database.shows;
            List<Show> filtrirane_shows = new List<Show>();

            if ((!Request["type"].Equals("")) && (!Request["availability"].Equals("")))
            {
                if (Request["availability"].Equals("available"))
                {
                    if (Request["type"].Equals("Concert"))
                    {
                        foreach (Show s in shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Active) && (s.Type == Enums.ShowType.Concert))
                            {
                                filtrirane_shows.Add(s);
                            }
                        }
                    }
                    else if (Request["type"].Equals("Festival"))
                    {
                        foreach (Show s in shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Active) && (s.Type == Enums.ShowType.Festival))
                            {
                                filtrirane_shows.Add(s);
                            }
                        }

                    }
                    else if (Request["type"].Equals("Cinema"))
                    {
                        foreach (Show s in shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Active) && (s.Type == Enums.ShowType.Cinema))
                            {
                                filtrirane_shows.Add(s);
                            }
                        }

                    }
                    else if (Request["type"].Equals("Theatre"))
                    {
                        foreach (Show s in shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Active) && (s.Type == Enums.ShowType.Theatre))
                            {
                                filtrirane_shows.Add(s);
                            }
                        }

                    }
                }
                else if (Request["availability"].Equals("neavailable"))
                {
                    if (Request["type"].Equals("Concert"))
                    {
                        foreach (Show s in shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Inactive) && (s.Type == Enums.ShowType.Concert))
                            {
                                filtrirane_shows.Add(s);
                            }
                        }
                    }
                    else if (Request["type"].Equals("Festival"))
                    {
                        foreach (Show s in shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Inactive) && (s.Type == Enums.ShowType.Festival))
                            {
                                filtrirane_shows.Add(s);
                            }
                        }

                    }
                    else if (Request["type"].Equals("Cinema"))
                    {
                        foreach (Show s in shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Inactive) && (s.Type == Enums.ShowType.Cinema))
                            {
                                filtrirane_shows.Add(s);
                            }
                        }

                    }
                    else if (Request["type"].Equals("Theatre"))
                    {
                        foreach (Show s in shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Inactive) && (s.Type == Enums.ShowType.Theatre))
                            {
                                filtrirane_shows.Add(s);
                            }
                        }

                    }

                }

            }
            else if ((!Request["type"].Equals("")) && (Request["availability"].Equals("")))
            {
                if (Request["type"].Equals("Concert"))
                {
                    foreach (Show s in shows)
                    {
                        if (s.Type == Enums.ShowType.Concert)
                        {
                            filtrirane_shows.Add(s);
                        }
                    }
                }
                else if (Request["type"].Equals("Festival"))
                {
                    foreach (Show s in shows)
                    {
                        if (s.Type == Enums.ShowType.Festival)
                        {
                            filtrirane_shows.Add(s);
                        }
                    }

                }
                else if (Request["type"].Equals("Cinema"))
                {
                    foreach (Show s in shows)
                    {
                        if (s.Type == Enums.ShowType.Cinema)
                        {
                            filtrirane_shows.Add(s);
                        }
                    }

                }
                else if (Request["type"].Equals("Theatre"))
                {
                    foreach (Show s in shows)
                    {
                        if (s.Type == Enums.ShowType.Theatre)
                        {
                            filtrirane_shows.Add(s);
                        }
                    }

                }


            }
            else if ((Request["type"].Equals("")) && (!Request["availability"].Equals("")))
            {
                if (Request["availability"].Equals("available"))
                {
                    foreach (Show s in shows)
                    {
                        if (s.Status == Enums.ShowStatus.Active)
                        {
                            filtrirane_shows.Add(s);
                        }
                    }
                }
                else if (Request["availability"].Equals("neavailable"))
                {
                    foreach (Show s in shows)
                    {
                        if (s.Status == Enums.ShowStatus.Inactive)
                        {
                            filtrirane_shows.Add(s);
                        }
                    }
                }
            }

            ViewBag.shows = filtrirane_shows;
            return View("~/Views/Shows/ShowShows.cshtml");
        }


        public ActionResult PrikaziManifestaciju()
        {
            string naziv = Request["ID"];
            List<Show> shows = Database.shows;
            foreach (Show s in shows)
            {
                if (s.Name.Equals(Request["ID"]))
                {
                    ViewBag.showShows = s;
                }
            }
            return View("~/Views/Shows/ShowShows.cshtml");
        }
    }
}