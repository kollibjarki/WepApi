using BlankAPI.Models.EF;
using BlankAPI.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BlankAPI.Controllers
{
    [RoutePrefix("api/rating")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class RatingsController : ApiController
    {
        readonly private RatingQueries rq;
        public RatingsController()
        {
            rq = new RatingQueries();
        }
        [HttpPost]
        [Route("add")]
        public void Add(Ratings rating)
        {
            rq.AddRating(rating);
        }
    }
}
