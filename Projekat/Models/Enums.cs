using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    [Serializable]
    public class Enums
    {
        public enum Sex { Male, Female, Other }

        public enum Role { Admin, Seller, Buyer }

        public enum ShowType { Concert, Festival, Theatre, Cinema }

        public enum ShowStatus { Active, Inactive }

        public enum TicketStatus { Reserved, Cancelled }

        public enum TicketType { VIP, Regular, FanPit }

    }
}