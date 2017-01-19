using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Models.DTO
{
    public class StaffDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public int Age { get; set; }
        public string PersonalQuote { get; set; }
        public string ImageUrl { get; set; }


    }
}