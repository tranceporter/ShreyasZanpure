using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SohamsFirstBirthday.Models
{
    public class RsvpViewModel
    {
        public string FamilyName { get; set; }
        public bool Attending { get; set; }
        public bool Vegetarian { get; set; }
    }
}