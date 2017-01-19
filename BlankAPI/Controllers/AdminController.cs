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
    [RoutePrefix("api/admin")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class AdminController : ApiController
    {
        readonly private AdminQueries aq;
        public AdminController()
        {
            aq = new AdminQueries();
        }
        [HttpPost]
        [Route("edit")]
        public void Edit(Product product)
        {
            aq.EditProduct(product);
        }
        [HttpPost]
        [Route("create")]
        public void Create(Product product)
        {
            DateTime localDate = DateTime.Now;
            product.ProductInfo.DateAdded = localDate;
            aq.AddNewProduct(product);

            //if (ModelState.IsValid)       //laga seinna
            //{
            //    pq.AddNewProduct(product);
            //}
        }
        [HttpPost]
        [Route("remove/{id}")]
        public void RemoveProduct(int id)
        {
            aq.RemoveProduct(id);
        }
    }
}
