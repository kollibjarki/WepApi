﻿using BlankAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Models.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        public int ProductId { get; set; }
        public int NumberOfLikes { get; set; }
        public DateTime DateSubmitted { get; set; }
        public virtual ICollection<SubComments> SubCom { get; set; }

    }
}