using System.Collections.Generic;
using System.Linq;
using BlankAPI.Models.EF;
using BlankAPI.Models.DTO;
using System;

namespace BlankAPI.Queries
{
    public class AdminQueries
    {
        private blankdbEntities _db;

        private string defaultDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.";
        public AdminQueries()
        {
            _db = new blankdbEntities();
        }
        public void AddNewProduct(Product product) //virkar post "create"
        {
            if (product == null)
            {
                return;
            }
            var category = (from x in _db.Category
                            where x.Name == product.Category.Name
                            select x).FirstOrDefault();
            if (category != null)
            {
                product.Category = category;
            }
            if(product.ProductInfo.Description == null)
            {
                product.ProductInfo.Description = defaultDescription;
            }
            _db.Product.Add(product);
            _db.SaveChanges();
        }
        public Product EditProduct(Product product) //virkar
        {
            var dbProduct = (from x in _db.Product
                           where x.Id == product.Id
                           select x).FirstOrDefault();
            dbProduct.Name = product.Name;
            dbProduct.CategoryId = product.CategoryId;
            dbProduct.ProductInfo.Price = product.ProductInfo.Price;
            dbProduct.ProductInfo.ImageUrl = product.ProductInfo.ImageUrl;
            dbProduct.ProductInfo.Description = product.ProductInfo.Description;
            dbProduct.ProductInfo.Discount = product.ProductInfo.Discount;
            if (dbProduct.ProductInfo.Description == null)
            {
                dbProduct.ProductInfo.Description = defaultDescription;
            }
            _db.SaveChanges();

            return product;
        }
        public void RemoveProduct(int id) //virkar 
        {
            var product = (from x in _db.Product
                         where x.Id == id
                         select x).FirstOrDefault();
            if (product != null)
            {
                var cleanBasket = (from x in _db.Basket //Athugar hvort item se til í basket
                           where x.ProductId == id
                           select x);
                foreach (Basket x in cleanBasket)
                {
                    _db.Basket.Remove(x);
                }
                var cleanComments = (from x in _db.Comments //Athugar hvort það sé comment á vöru
                                   where x.ProductId == id
                                   select x);
                foreach (Comments x in cleanComments)
                {
                    var cleanLikes = (from y in _db.Likes  //Athugar hvort það sé like á comment
                                      where y.CommentId == x.Id
                                      select y);
                    foreach (Likes y in cleanLikes)
                    {
                        _db.Likes.Remove(y);
                    }
                    _db.Comments.Remove(x);
                }
                var cleanRatings = (from x in _db.Ratings //Athugar hvort það séu til ratings
                                   where x.ProductId == id
                                   select x);
                foreach(Ratings x in cleanRatings)
                {
                    _db.Ratings.Remove(x);
                }
                var cleanInfoId = (from x in _db.Product
                                   where x.Id == id
                                   select x.InfoId).FirstOrDefault(); //Athugar info Id
                var cleanInfo = (from x in _db.ProductInfo
                                 where x.Id == cleanInfoId
                                 select x);                     //Finnur info Id i ProductInfo
                foreach (ProductInfo x in cleanInfo)
                {
                    _db.ProductInfo.Remove(x);
                }
                _db.Product.Remove(product);
                _db.SaveChanges();
            }
        }
    }
}