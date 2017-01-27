using BlankAPI.Queries;
using BlankAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BlankAPI.Models.DTO;

namespace BlankAPI.Controllers
{
    [RoutePrefix("api/cart")]
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class CartController : ApiController
    {
        readonly private CartQueries cq;
        public CartController()
        {
            cq = new CartQueries();
        }
        [HttpPost]
        [Route("add")]
        public void Add(Basket basketItem)
        {
            cq.AddToBasket(basketItem);
        }
        [HttpPost]
        [Route("transferBasket/{userName}")]
        public void Transfer(string userName, TransferDTO basketItems)
        {
            cq.TransferGuestBasket(userName, basketItems);
        }
        [HttpPost]
        [Route("remove")]
        public void Remove(Basket basketitem)
        {
            cq.RemoveSingleFromBasket(basketitem);
        }
        [HttpPost]
        [Route("removeItem")]
        public void RemoveItem(Basket basketitem)
        {
            cq.RemoveItemFromBasket(basketitem);
        }
        [HttpGet]
        [Route("getbasket/{userName}")]
        public IEnumerable<ProductDTO> GetBasket(string userName)
        {
            return cq.GetItemsInBasket(userName);
        }
        [HttpPost]
        [Route("empty/{userName}")]
        public void Empty(string userName)
        {
            cq.EmptyBasket(userName);
        }
    }
}
