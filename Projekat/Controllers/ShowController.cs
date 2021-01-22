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

            DateTime dateFrom;
            DateTime dateTo;

            int priceFrom;
            int priceTo;

            int casee = 1;

            bool name, date1, date2, price1, price2;

            name = Request["search_value"] != "";
            date1 = DateTime.TryParse(Request["dateFrom"], out dateFrom);
            date2 = DateTime.TryParse(Request["dateTo"], out dateTo);
            price1 = Int32.TryParse(Request["priceFrom"], out priceFrom);
            price2 = Int32.TryParse(Request["priceTo"], out priceTo);

            if (name || (date1 && date2) || (price1 && price2))
            {
                if (name && (date1 && date2) && (price1 && price2))
                    casee = 2;
                else if (name && (date1 && date2))
                    casee = 3;
                else if (name && (price1 && price2))
                    casee = 4;
                else if ((date1 && date2) && (price1 && price2))
                    casee = 5;
                else if (name)
                    casee = 6;
                else if (date1 && date2)
                    casee = 7;
                else if (price1 && price2)
                    casee = 8;
            }
            else
                casee = 1;



            foreach (Show s in Database.shows)
            {
                DateTime date_shows = s.Start;

                int compare_dateFrom = DateTime.Compare(date_shows, dateFrom);
                int compare_dateTo = DateTime.Compare(date_shows, dateTo);

                switch (casee)
                {
                    case 1:

                        ViewBag.shows = Database.shows;
                        break;

                    case 2:

                        if ((s.Name.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.City.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.Street.ToUpper().Equals(Request["search_value"].ToUpper())))
                        {
                            if ((compare_dateFrom >= 0) && (compare_dateTo <= 0) && (s.Price >= priceFrom) && (s.Price <= priceTo))
                            {
                                if (!searchedShows.Contains(s))
                                {
                                    searchedShows.Add(s);
                                }
                            }
                        }
                        break;
                    case 3:

                        if ((s.Name.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.City.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.Street.ToUpper().Equals(Request["search_value"].ToUpper())))
                        {
                            if ((compare_dateFrom >= 0) && (compare_dateTo <= 0))
                            {
                                if (!searchedShows.Contains(s))
                                {
                                    searchedShows.Add(s);
                                }
                            }
                        }
                        break;

                    case 4:

                        if ((s.Name.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.City.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.Street.ToUpper().Equals(Request["search_value"].ToUpper())))
                        {
                            if ((s.Price >= priceFrom) && (s.Price <= priceTo))
                            {
                                if (!searchedShows.Contains(s))
                                {
                                    searchedShows.Add(s);
                                }
                            }
                        }
                        break;

                    case 5:

                        if ((compare_dateFrom >= 0) && (compare_dateTo <= 0) && (s.Price >= priceFrom) && (s.Price <= priceTo))
                        {
                            if (!searchedShows.Contains(s))
                            {
                                searchedShows.Add(s);
                            }
                        }
                        break;

                    case 6:

                        if ((s.Name.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.City.ToUpper().Equals(Request["search_value"].ToUpper())) || (s.Address.Street.ToUpper().Equals(Request["search_value"].ToUpper())))
                        {
                            if (!searchedShows.Contains(s))
                            {
                                searchedShows.Add(s);
                            }
                        }
                        break;

                    case 7:

                        if ((compare_dateFrom >= 0) && (compare_dateTo <= 0))
                        {
                            if (!searchedShows.Contains(s))
                            {
                                searchedShows.Add(s);
                            }
                        }
                        break;

                    case 8:

                        if ((s.Price >= priceFrom) && (s.Price <= priceTo))
                        {
                            if (!searchedShows.Contains(s))
                            {
                                searchedShows.Add(s);
                            }
                        }
                        break;

                    default:
                        break;
                }

            }
            ViewBag.shows = searchedShows;
            return View("~/Views/Shows/ShowShows.cshtml");
        }

       
        public ActionResult Sort()
        {
            List<Show> shows = Database.shows;
            List<Show> sortedShows = new List<Show>();

            if (Request["sortBy"].Equals("name"))
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
            else if (Request["sortBy"].Equals("dateITime"))
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

        public ActionResult Filter()
        {
            List<Show> shows = Database.shows;
            List<Show> filtered_shows = new List<Show>();

            if ((!Request["type"].Equals("")) && (!Request["availability"].Equals("")))
            {
                if (Request["availability"].Equals("available"))
                {
                    if (Request["type"].Equals("concert"))
                    {
                        foreach (Show s in Database.shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Active) && (s.Type == Enums.ShowType.Concert))
                            {
                                filtered_shows.Add(s);
                            }
                        }
                    }
                    else if (Request["type"].Equals("festival"))
                    {
                        foreach (Show s in Database.shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Active) && (s.Type == Enums.ShowType.Festival))
                            {
                                filtered_shows.Add(s);
                            }
                        }

                    }
                    else if (Request["type"].Equals("cinema"))
                    {
                        foreach (Show s in Database.shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Active) && (s.Type == Enums.ShowType.Cinema))
                            {
                                filtered_shows.Add(s);
                            }
                        }

                    }
                    else if (Request["type"].Equals("theatre"))
                    {
                        foreach (Show s in Database.shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Active) && (s.Type == Enums.ShowType.Theatre))
                            {
                                filtered_shows.Add(s);
                            }
                        }

                    }
                }
                else if (Request["availability"].Equals("available"))
                {
                    if (Request["type"].Equals("concert"))
                    {
                        foreach (Show s in Database.shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Inactive) && (s.Type == Enums.ShowType.Concert))
                            {
                                filtered_shows.Add(s);
                            }
                        }
                    }
                    else if (Request["type"].Equals("festival"))
                    {
                        foreach (Show s in Database.shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Inactive) && (s.Type == Enums.ShowType.Festival))
                            {
                                filtered_shows.Add(s);
                            }
                        }

                    }
                    else if (Request["type"].Equals("cinema"))
                    {
                        foreach (Show s in Database.shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Inactive) && (s.Type == Enums.ShowType.Cinema))
                            {
                                filtered_shows.Add(s);
                            }
                        }

                    }
                    else if (Request["type"].Equals("theatre"))
                    {
                        foreach (Show s in Database.shows)
                        {
                            if ((s.Status == Enums.ShowStatus.Inactive) && (s.Type == Enums.ShowType.Theatre))
                            {
                                filtered_shows.Add(s);
                            }
                        }

                    }

                }

            }
            else if ((!Request["type"].Equals("")) && (Request["availability"].Equals("")))
            {
                if (Request["type"].Equals("concert"))
                {
                    foreach (Show s in Database.shows)
                    {
                        if (s.Type == Enums.ShowType.Concert)
                        {
                            filtered_shows.Add(s);
                        }
                    }
                }
                else if (Request["type"].Equals("festival"))
                {
                    foreach (Show s in Database.shows)
                    {
                        if (s.Type == Enums.ShowType.Festival)
                        {
                            filtered_shows.Add(s);
                        }
                    }

                }
                else if (Request["type"].Equals("cinema"))
                {
                    foreach (Show s in Database.shows)
                    {
                        if (s.Type == Enums.ShowType.Cinema)
                        {
                            filtered_shows.Add(s);
                        }
                    }

                }
                else if (Request["type"].Equals("theatre"))
                {
                    foreach (Show s in Database.shows)
                    {
                        if (s.Type == Enums.ShowType.Theatre)
                        {
                            filtered_shows.Add(s);
                        }
                    }

                }


            }
            else if ((Request["type"].Equals("")) && (!Request["availability"].Equals("")))
            {
                if (Request["availability"].Equals("available"))
                {
                    foreach (Show s in Database.shows)
                    {
                        if (s.Status == Enums.ShowStatus.Active)
                        {
                            filtered_shows.Add(s);
                        }
                    }
                }
                else if (Request["availability"].Equals("unavailable"))
                {
                    foreach (Show s in Database.shows)
                    {
                        if (s.Status == Enums.ShowStatus.Inactive)
                        {
                            filtered_shows.Add(s);
                        }
                    }
                }
            }

            ViewBag.shows = filtered_shows;
            return View("~/Views/Shows/ShowShows.cshtml");
        }


        public ActionResult ShowShow()
        {
            Database.ReadData();
            List<Show> shows = Database.shows;
            foreach (Show s in shows)
            {
                if (s.Name.Equals(Request["ID"]))
                {
                    ViewBag.showShows = s;
                }
            }
            return View("~/Views/Shows/ShowShow.cshtml");
        }
    }
}