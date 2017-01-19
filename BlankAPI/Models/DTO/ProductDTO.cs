using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
        public int? DiscPrice { get; set; }
        public string Description { get; set; }
        public int? Discount { get; set; }
        public int Views { get; set; }
        public double AvgRating { get; set; }
        public double RatingVotes { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public DateTime DateAdded { get; set; }
    }
}