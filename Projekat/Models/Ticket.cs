using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Projekat.Models.Enums;

namespace Projekat.Models
{
    public class Ticket
    {
        public string ID { get; set; }

        public Show Show { get; set; }

        public DateTime Time { get; set; }

        public float Price { get; set; }

        public string Buyer { get; set; }

        public TicketStatus Status { get; set; }

        public ShowType Type { get; set; }

        public Ticket()
        {

        }

        public Ticket(string id, Show show, DateTime time, float price, string buyer, TicketStatus status, ShowType type)
        {
            ID = id;
            Show = show;
            Time = time;
            Price = price;
            Buyer = buyer;
            Status = status;
            Type = type;
        }
    }
}