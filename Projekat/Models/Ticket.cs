using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Projekat.Models.Enums;

namespace Projekat.Models
{
    [Serializable]
    public class Ticket
    {
        public int ID { get; set; }

        public Show Show { get; set; }

        public DateTime Time { get; set; }

        public float Price { get; set; }

        public int Amount { get; set; }

        public float TotalPrice { get; set; }

        public string Buyer { get; set; }

        public TicketStatus Status { get; set; }

        public ShowType Type { get; set; }

        public TicketType TicketType { get; set; }

        public Ticket()
        {

        }

        public Ticket(Show show, DateTime time, float price, string buyer, TicketStatus status, ShowType type, int amount)
        {
            ID = -1;
            Show = show;
            Time = time;
            Price = price;
            Buyer = buyer;
            Status = status;
            Type = type;
            Amount = amount;
            TotalPrice = Price * Amount;
        }
    }
}