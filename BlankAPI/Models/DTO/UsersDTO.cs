using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Models.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string PersonalQuote { get; set; }
        public string ImageUrl { get; set; }


    }
}