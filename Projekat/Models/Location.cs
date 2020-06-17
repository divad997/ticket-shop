using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class Location
    {
        public float Height { get; set; }
        
        public float Width { get; set; }

        public Address Address { get; set; }

        public Location()
        {

        }

        public Location(float height, float width, Address address)
        {
            Height = height;
            Width = width;
            Address = address;
        }

    }
}