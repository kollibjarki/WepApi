using BlankAPI.Models.DTO;
using BlankAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Queries
{
    public class OrdersQueries
    {
        private blankdbEntities _db;
        public OrdersQueries()
        {
            _db = new blankdbEntities();
        }

        public IEnumerable<OrderDTO> GetOrders(string userName)
        {
            var items = from x in _db.Orders
                        where userName == x.UserId
                        select new OrderDTO()
                        {
                            Id = x.Product.Id,
                            Name = x.Product.Name,
                            Price = x.Product.ProductInfo.Price,
                            DiscPrice = x.Product.ProductInfo.Price - (x.Product.ProductInfo.Price * x.Product.ProductInfo.Discount / 100),
                            Discount = x.Product.ProductInfo.Discount,
                            CategoryName = x.Product.Category.Name,
                            Quantity = x.Quantity,
                            DateOrdered = x.OrderDate,
                            DeliveryDate = x.DeliveryDate,
                            ImageUrl = x.Product.ProductInfo.ImageUrl
                        };
            return items;
        }

        private Orders order;
        public void PlaceOrder(string userName, TransferDTO basketItems)
        {
            DateTime localDate = DateTime.Now;
            var idAndQty = basketItems.ProductId.Zip(basketItems.Quantity, (i, q) => new { Id = i, Qty = q });
            foreach (var iq in idAndQty)
            {
                var orders = (from x in _db.Basket
                              where x.UserId == userName && x.ProductId == iq.Id
                              select x).FirstOrDefault();
                order = new Orders()
                {
                    UserId = userName,
                    ProductId = iq.Id,
                    Quantity = iq.Qty,
                    OrderDate = localDate,
                    DeliveryDate = localDate.AddDays(14)
                };
                _db.Orders.Add(order);

                var countBought = (from x in _db.Product //telja hversu oft product er ordered
                            where x.Id == iq.Id
                            select x).FirstOrDefault();
                countBought.ProductInfo.Bought += iq.Qty;
            }

            var product = (from x in _db.Basket
                           where x.UserId == userName
                           select x).ToArray();
            foreach (Basket p in product)
            {
                _db.Basket.Remove(p);
            }
            _db.SaveChanges();
        }
    }
}