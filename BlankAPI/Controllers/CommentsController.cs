using BlankAPI.Models.DTO;
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
    [RoutePrefix("api/comments")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CommentsController : ApiController
    {
        private readonly CommentQueries cq;
        public CommentsController()
        {
            cq = new CommentQueries();
        }

        [HttpPost]
        [Route("add")]
        public void AddComment(Comments comment)
        {
            cq.AddNewComment(comment);
        }
        [HttpPost]
        [Route("addsubcomment")]
        public void AddSubComment(SubComments subComment)
        {
            cq.AddNewSubComment(subComment);
        }
        [HttpPost]
        [Route("remove/{id}")]
        public void RemoveComment(int id)
        {
            cq.RemoveComment(id);
        }
        [HttpPost]
        [Route("removeSub/{id}")]
        public void RemoveSubComment(int id)
        {
            cq.RemoveSubComment(id);
        }
        [HttpGet]
        [Route("getComments/{id}")]
        public IEnumerable<CommentDTO> GetComments(int id)
        {
            return cq.GetComments(id);
        }
        [HttpPost]
        [Route("like")]
        public void LikeComment(Likes like)
        {
            cq.ToggleLikeComment(like);
        }
    }
}
