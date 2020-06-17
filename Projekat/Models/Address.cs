using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekat.Models
{
    public class Address
    {
        public string Street { get; set; }

        public string City { get; set; }

        public int ZipCode { get; set; }


        public Address()
        {

        }

        public Address(string street, string city, int zip)
        {
            Street = street;
            City = city;
            ZipCode = zip;
        }

    }
}