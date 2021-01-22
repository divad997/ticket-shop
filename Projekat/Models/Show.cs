using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using static Projekat.Models.Enums;

namespace Projekat.Models
{
    [Serializable]
    public class Show
    {
        public string Name { get; set; }

        public Enums.ShowType Type { get; set; }

        public int NumberOfSeats { get; set; }

        public DateTime Start { get; set; }

        public TimeSpan Duration { get; set; }

        public float Price { get; set; }

        public ShowStatus Status { get; set; }

        public Address Address { get; set; }

        public float Rating { get; set; }

        public List<Comment> Comments { get; set; }

        public string Picture { get; set; }

        public Show()
        {

        }

        public Show(string name, ShowType type, int number, DateTime start, float price, ShowStatus status, Address address, string picture)
        {
            Name = name;
            Type = type;
            NumberOfSeats = number;
            Start = start;
            Price = price;
            Status = status;
            Address = address;
            Comments = new List<Comment>();
            Picture = picture;
        }

        public float CalculateRating()
        {
            Database.ReadData();
            int i = 0;
            int value = 0;
            
            foreach (Comment c in Database.comments)
            {
                if (c.Show == this.Name)
                {
                    value += c.Rating;
                    i++;
                }
            }
            if(i != 0)
            {
                value = value / i;
                return value;
            }
            return value;
        }
    }
}