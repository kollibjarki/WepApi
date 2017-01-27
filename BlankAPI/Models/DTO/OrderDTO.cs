using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Models.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public int Price { get; set; }
        public int? DiscPrice { get; set; }
        public int? Discount { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
