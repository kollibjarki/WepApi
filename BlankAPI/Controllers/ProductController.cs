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
    [RoutePrefix("api/product")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ProductController : ApiController
    {
        readonly private ProductQueries pq;
        public ProductController()
        {
            pq = new ProductQueries();
        }
        [HttpGet]
        [Route("getCategories")]
        public IEnumerable<CategoryDTO> GetCategories()
        {
            return pq.GetCat();
        }
        [HttpGet]
        [Route("getsearch")]
        public IEnumerable<ProductDTO> GetForSearch()
        {
            return pq.GetAllForSearch();
        }
        [HttpGet]
        [Route("getpopular")]
        public IEnumerable<ProductDTO> GetPopular()
        {
            return pq.GetPopular();
        }
        [HttpGet]
        [Route("getonsale")]
        public IEnumerable<ProductDTO> GetOnSale()
        {
            return pq.GetOnSale();
        }
        [HttpGet]
        [Route("category/{categoryName}")]
        public IEnumerable<ProductDTO> GetProducts(string categoryName)
        {
            return pq.GetList(categoryName);
        }
        [HttpGet]
        [Route("item/{id}")]
        public ProductDTO GetItemById(int id)
        {
            return pq.GetById(id);
        }
    }
}
