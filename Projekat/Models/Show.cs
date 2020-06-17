using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using static Projekat.Models.Enums;

namespace Projekat.Models
{
    public class Show
    {
        public string Name { get; set; }

        public Enums.ShowType Type { get; set; }

        public int NumberOfSeats { get; set; }

        public DateTime Start { get; set; }

        public float Price { get; set; }

        public ShowStatus Status { get; set; }

        public Address Address { get; set; }

        public Image Picture { get; set; }

        public Show()
        {

        }

        public Show(string name, ShowType type, int number, DateTime start, float price, ShowStatus status, Address address, Image picture)
        {
            Name = name;
            Type = type;
            NumberOfSeats = number;
            Start = start;
            Price = price;
            Status = status;
            Address = address;
            Picture = picture;
        }
    }
}