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
    
    public partial class SubComments
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public System.DateTime DateSubmitted { get; set; }
        public int NumberOfLikes { get; set; }
    }
}