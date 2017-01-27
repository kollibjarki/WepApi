using BlankAPI.Models.DTO;
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
    [RoutePrefix("api/orders")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class OrdersController : ApiController
    {
        private OrdersQueries oq;
        public OrdersController()
        {
            oq = new OrdersQueries();
        }
        [HttpPost]
        [Route("placeorder/{userName}")]
        public void PlaceOrder(string userName, TransferDTO basketItems)
        {
            oq.PlaceOrder(userName, basketItems);
        }
        [HttpGet]
        [Route("getorders/{userName}")]
        public IEnumerable<OrderDTO> GetOrders(string userName)
        {
            return oq.GetOrders(userName);
        }
    }
}
