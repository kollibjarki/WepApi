using BlankAPI.Models.DTO;
using BlankAPI.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlankAPI.Queries
{
    public class CartQueries
    {
        private blankdbEntities _db;
        public CartQueries()
        {
            _db = new blankdbEntities();
        }
        public void AddToBasket(Basket basketItem)
        {
            var product = (from x in _db.Basket
                           where x.UserId == basketItem.UserId && x.ProductId == basketItem.ProductId
                           select x).FirstOrDefault();
            if (product != null)
            {
                product.Quantity += basketItem.Quantity;
            }
            if (product == null)
            {
                basketItem.Quantity = basketItem.Quantity;
                _db.Basket.Add(basketItem);
            }
            _db.SaveChanges();
        }
        private Basket item;
        public void TransferGuestBasket(string userName, TransferDTO basketItems)
        {
            var idAndQty = basketItems.ProductId.Zip(basketItems.Quantity, (i, q) => new { Id = i, Qty = q });
            foreach (var iq in idAndQty)
            {
                var product = (from x in _db.Basket
                               where x.UserId == userName && x.ProductId == iq.Id
                               select x).FirstOrDefault();
                if(product == null)
                {
                    item = new Basket()
                    {
                        UserId = userName,
                        ProductId = iq.Id,
                        Quantity = iq.Qty
                    };
                    _db.Basket.Add(item);
                }
                if(product != null)
                {
                    product.Quantity += iq.Qty;
                }
            }
            _db.SaveChanges();
            
        }
        public void RemoveSingleFromBasket(Basket basketitem)
        {
            var product = (from x in _db.Basket
                           where x.UserId == basketitem.UserId && x.ProductId == basketitem.ProductId
                           select x).FirstOrDefault();
            if (product != null)
            {
                if (product.Quantity == 1)
                {
                    _db.Basket.Remove(product);
                }
                if(product.Quantity > 1)
                {
                    product.Quantity--;
                }
                _db.SaveChanges();
            }
        }
        public void RemoveItemFromBasket(Basket basketitem)
        {
            var product = (from x in _db.Basket
                           where x.UserId == basketitem.UserId && x.ProductId == basketitem.ProductId
                           select x).FirstOrDefault();
            if (product != null)
            {
                _db.Basket.Remove(product);
                _db.SaveChanges();
            }
        }
        public IEnumerable<ProductDTO> GetItemsInBasket(string UserName)
        {
            var items = from x in _db.Basket
                             where UserName == x.UserId
                             select new ProductDTO() {
                                 Id = x.ProductId,
                                 Name = x.Product.Name,
                                 Price = x.Product.ProductInfo.Price,
                                 DiscPrice = x.Product.ProductInfo.Price - (x.Product.ProductInfo.Price * x.Product.ProductInfo.Discount / 100),
                                 Discount = x.Product.ProductInfo.Discount,
                                 CategoryName = x.Product.Category.Name,
                                 Quantity = x.Quantity,
                                 Description = x.Product.ProductInfo.Description,
                                 ImageUrl = x.Product.ProductInfo.ImageUrl
                             };
            return items;
        }

        public void EmptyBasket(string UserName)
        {
            var basketItems = from x in _db.Basket
                              where x.UserId == UserName
                              select x;
            foreach(Basket x in basketItems)
            {
                _db.Basket.Remove(x);
            }
            _db.SaveChanges();
        }
    }

}
