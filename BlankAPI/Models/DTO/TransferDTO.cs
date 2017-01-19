using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Models.DTO
{
    public class TransferDTO
    {
        public int[] ProductId { get; set; }
        public int[] Quantity { get; set; }
    }
}