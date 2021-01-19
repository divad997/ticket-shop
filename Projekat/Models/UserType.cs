using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    [Serializable]
    public class UserType
    {
        public string Name { get; set; }

        public float Discount { get; set; }

        public int ReqPoints { get; set; }

        public UserType()
        {

        }

        public UserType(string name, float discount, int req)
        {
            Name = name;
            Discount = discount;
            ReqPoints = req;

        }

    }
}