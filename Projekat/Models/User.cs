using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Projekat.Models.Enums;

namespace Projekat.Models
{
    public class User
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }

        public DateTime Birthday { get; set; }

        public Role Role { get; set; }

        List<Ticket> Tickets { get; set; }

        List<Show> Shows { get; set; }

        public int Points { get; set; }

        public UserType Type { get; set; }


        public User()
        {

        }

        public User(string user, string pass, string name, string surname, DateTime birth, Role role, List<Ticket> tickets, List<Show> shows, int points, UserType type)
        {
            Username = user;
            Password = pass;
            Name = name;
            Surname = surname;
            Birthday = birth;
            Role = role;
            Tickets = tickets;
            Shows = shows;
            Points = points;
            Type = type;
        }
    }
}