using BlankAPI.Models.DTO;
using BlankAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Queries
{
    public class CommentQueries
    {
        private blankdbEntities _db;

        public CommentQueries()
        {
            _db = new blankdbEntities();
        }

        public void AddNewComment(Comments comment)
        {
            DateTime localDate = DateTime.Now;
            comment.DateSubmitted = localDate;
            if(comment.UserId == "Kolbeinn" || comment.UserId == "Diddi")
            {
                comment.UserId = "Blank Store";
            }
            _db.Comments.Add(comment);
            _db.SaveChanges();
        }
        public void AddNewSubComment(SubComments subComment)
        {
            DateTime localDate = DateTime.Now;
            subComment.DateSubmitted = localDate;
            if (subComment.UserId == "Kolbeinn" || subComment.UserId == "Diddi")
            {
                subComment.UserId = "Blank Store";
            }
            _db.SubComments.Add(subComment);
            _db.SaveChanges();
        }
        public void RemoveComment(int id)
        {
            var getLikes = from x in _db.Likes  //finna og eyða likes (if any)
                           where x.CommentId == id
                           select x;
            foreach(Likes x in getLikes)
            {
                _db.Likes.Remove(x);
            }
            var getSub = from x in _db.SubComments  //finna og eyða subcomments (if any)
                         where x.CommentId == id
                         select x;
            foreach (SubComments x in getSub)
            {
                _db.SubComments.Remove(x);
            }
            var comment = (from x in _db.Comments
                           where id == x.Id
                           select x).FirstOrDefault();
            _db.Comments.Remove(comment);
            _db.SaveChanges();
        }
        public void RemoveSubComment(int id)
        {
            var subComment = (from x in _db.SubComments  //finna og eyða subcomments (if any)
                         where x.Id == id
                         select x).FirstOrDefault();
            _db.SubComments.Remove(subComment);
            _db.SaveChanges();
        }
        public IQueryable<CommentDTO> GetComments(int id)
        {
            var com = from x in _db.Comments
                      where x.ProductId == id
                      select new CommentDTO()
                      {
                          Id = x.Id,
                          Comment = x.Comment,
                          UserId = x.UserId,
                          ProductId = x.ProductId,
                          NumberOfLikes = x.NumberOfLikes,
                          DateSubmitted = x.DateSubmitted,
                          SubCom = x.SubComments
                      };
            return com;
        } 

        public void ToggleLikeComment(Likes like)
        {
            var checkOldlike = (from x in _db.Likes
                        where x.CommentId == like.CommentId && x.UserId == like.UserId
                        select x).FirstOrDefault();
            var setNumberOfLikes = (from x in _db.Comments
                                where x.Id == like.CommentId
                                select x).FirstOrDefault();
            if (checkOldlike == null)
            {
                setNumberOfLikes.NumberOfLikes++;
                _db.Likes.Add(like);
            }
            if (checkOldlike != null)
            {
                setNumberOfLikes.NumberOfLikes--;
                _db.Likes.Remove(checkOldlike);
            }
            _db.SaveChanges();
        }

    }
}