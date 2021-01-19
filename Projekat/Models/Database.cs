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
                tickets = new List<Ticket>();
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