//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlankAPI.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Ratings
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public double Rating { get; set; }
    
        public virtual Product Product { get; set; }
    }
}
