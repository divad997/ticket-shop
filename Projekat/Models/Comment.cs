using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class Comment
    {
        public string Buyer { get; set; }

        public string Show { get; set; }

        public string Post { get; set; }

        public int Rating { get; set; }

        public Comment()
        {

        }

        public Comment(string buyer, string show, string post, int rating)
        {
            Buyer = buyer;
            Show = show;
            Post = post;
            Rating = rating;
        }
    }
}