using BlankAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Queries
{
    public class RatingQueries
    {
        private blankdbEntities _db;
        public RatingQueries()
        {
            _db = new blankdbEntities();
        }
        public void AddRating(Ratings rating)
        {
            var getrating = (from x in _db.Ratings
                           where x.UserId == rating.UserId && x.ProductId == rating.ProductId
                           select x).FirstOrDefault();
            if (getrating != null)
            {
                getrating.Rating = rating.Rating;
                _db.SaveChanges();
            }
            if (getrating == null)
            {
                _db.Ratings.Add(rating);
                _db.SaveChanges();
            }
        }
    }
}