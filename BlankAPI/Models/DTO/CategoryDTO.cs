using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Models.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }

    }
}