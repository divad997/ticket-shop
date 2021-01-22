using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Xml;
using Projekat.Models;

namespace Projekat.Models
{
    public class Database
    {
        public static string path = @"E:\Faks\WEB 1\Projekat\Projekat\App_Data\";
        public static List<Ticket> tickets = new List<Ticket>();
        public static List<User> users = new List<User>();
        public static List<Show> shows = new List<Show>();
        public static List<Show> showshold = new List<Show>();
        public static List<Comment> comments = new List<Comment>();
        public static List<Comment> commentshold = new List<Comment>();
        public static List<Comment> commentsrejected = new List<Comment>();

        User admin = new User("admin", "admin", "Admir", "Admirovic", new DateTime(1997, 9, 30), Enums.Role.Admin, 99999, new UserType("Gold", 100, 0));
        User seller = new User("seller", "seller", "Heller", "Seller", new DateTime(1997, 9, 30), Enums.Role.Seller, 99999, new UserType("Gold", 100, 0));
        User user = new User("divad997", "dadada97", "David", "Jovanovski", new DateTime(1997, 9, 30), Enums.Role.Buyer, 3001, new UserType("Silver", 3, 4000));

        


        public static string serializationFileTickets = path + "ResourcesTickets";
        public static string serializationFileUsers = path + "ResourcesUsers";
        public static string serializationFileShows = path + "ResourcesShows";
        public static string serializationFileShowsOnHold = path + "ResourcesShowsOnHold";
        public static string serializationFileComments = path + "ResourcesComments";
        public static string serializationFileCommentsOnHold = path + "ResourcesCommentsOnHold";
        public static string serializationFileCommentsRejected = path + "ResourcesCommentsRejected";
       

        public static void ReadData()
        {
            
            try
            {
                using (Stream stream = File.Open(serializationFileTickets, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();

                    tickets = (List<Ticket>)bformatter.Deserialize(stream);
                }
            }
            catch (Exception exc)
            {
                User admin = new User("admin", "admin", "Admir", "Admirovic", new DateTime(1997, 9, 30), Enums.Role.Admin, 99999, new UserType("Gold", 100, 0));
                User seller = new User("seller", "seller", "Heller", "Seller", new DateTime(1997, 9, 30), Enums.Role.Seller, 99999, new UserType("Gold", 100, 0));
                User user = new User("divad997", "dadada97", "David", "Jovanovski", new DateTime(1997, 9, 30), Enums.Role.Buyer, 3001, new UserType("Silver", 3, 4000));

                users.Add(admin);
                users.Add(seller);
                users.Add(user);

                Show m1 = new Show("Lord of the Rings: Two Towers", Enums.ShowType.Cinema, 25, DateTime.Now.AddHours(4), 350, Enums.ShowStatus.Active, new Address("Bulevar Mihajla Pupina 3", "Novi Sad", 21000), "twotower.jpg");
                Show f1 = new Show("Exit:2021", Enums.ShowType.Festival, 10000, new DateTime(2021, 7, 14, 19, 0, 0), 5999, Enums.ShowStatus.Active, new Address("Petrovaradin Fortress", "Novi Sad", 21000), "egzit.jpg");
                Show c1 = new Show("The Weeknd", Enums.ShowType.Concert, 3, DateTime.Now.AddDays(1), 5999, Enums.ShowStatus.Active, new Address("Stark Arena", "Beograd", 11000), "theweeknd.jpg");
                Show t1 = new Show("The Barber of Seville", Enums.ShowType.Theatre, 10, DateTime.Now.AddDays(2), 1200, Enums.ShowStatus.Active, new Address("Pozorisni Trg 1", "Beograd", 11000), "barber.jpg");
                Show m2 = new Show("Lord of the Rings: Fellowshit of the Ring", Enums.ShowType.Cinema, 25, DateTime.Now.AddHours(4), 350, Enums.ShowStatus.Active, new Address("Bulevar Mihajla Pupina 3", "Novi Sad", 21000), "\twotower.jpg");
                Show f2 = new Show("Tomorrowland", Enums.ShowType.Festival, 10000, new DateTime(2021, 7, 14, 19, 0, 0), 5999, Enums.ShowStatus.Active, new Address("Petrovaradin Fortress", "Novi Sad", 21000), "exit.jpg");
                Show c2 = new Show("Ceca", Enums.ShowType.Concert, 3, DateTime.Now.AddDays(1), 5999, Enums.ShowStatus.Active, new Address("Stark Arena", "Beograd", 11000), "theweeknd.jpg");
                Show t2 = new Show("Shit", Enums.ShowType.Theatre, 10, DateTime.Now.AddDays(2), 1200, Enums.ShowStatus.Active, new Address("Pozorisni Trg 1", "Beograd", 11000), "barber.jpg");

                shows.Add(m1);
                shows.Add(f1);
                shows.Add(c1);
                shows.Add(t1);

                Database.UpdateData();
            }



            try
            {
                using (Stream stream = File.Open(serializationFileUsers, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();

                    users = (List<User>)bformatter.Deserialize(stream);
                }
            }
            catch (Exception exc)
            {
                users = new List<User>();
            }

            try
            {
                using (Stream stream = File.Open(serializationFileShows, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();

                    shows = (List<Show>)bformatter.Deserialize(stream);
                }
            }
            catch (Exception exc)
            {
                shows = new List<Show>();
            }

            try
            {
                using (Stream stream = File.Open(serializationFileComments, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();

                    comments = (List<Comment>)bformatter.Deserialize(stream);
                }
            }
            catch (Exception exc)
            {
                comments = new List<Comment>();
            }

            try
            {
                using (Stream stream = File.Open(serializationFileCommentsOnHold, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();

                    commentshold = (List<Comment>)bformatter.Deserialize(stream);
                }
            }
            catch (Exception exc)
            {
                commentshold = new List<Comment>();
            }

            try
            {
                using (Stream stream = File.Open(serializationFileShowsOnHold, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();

                    showshold = (List<Show>)bformatter.Deserialize(stream);
                }
            }
            catch (Exception exc)
            {
                showshold = new List<Show>();
            }

            try
            {
                using (Stream stream = File.Open(serializationFileCommentsRejected, FileMode.Open))
                {
                    BinaryFormatter bformatter = new BinaryFormatter();

                    commentsrejected = (List<Comment>)bformatter.Deserialize(stream);
                }
            }
            catch (Exception exc)
            {
                commentsrejected = new List<Comment>();
            }
        }

        public static void UpdateData()
        {

            using (Stream stream = File.Open(serializationFileTickets, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, tickets);
            }
            using (Stream stream = File.Open(serializationFileUsers, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, users);
            }
            using (Stream stream = File.Open(serializationFileComments, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, comments);
            }
            using (Stream stream = File.Open(serializationFileCommentsOnHold, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, commentshold);
            }
            using (Stream stream = File.Open(serializationFileCommentsRejected, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, commentsrejected);
            }
            using (Stream stream = File.Open(serializationFileShows, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, shows);
            }
            using (Stream stream = File.Open(serializationFileShowsOnHold, FileMode.Create))
            {
                BinaryFormatter bformatter = new BinaryFormatter();

                bformatter.Serialize(stream, showshold);
            }

        }

        public static bool UserExists(string username)
        {
            foreach(User u in users)
            {
                if (u.Username.Equals(username))
                    return true;
            }
            return false;
        }
    }

    

}