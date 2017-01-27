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

        public void DisableProduct(int id)
        {
            var product = (from x in _db.Product
                           where x.Id == id
                           select x).FirstOrDefault();
            if (product != null)
            {
                product.IsActive = 1;
                _db.SaveChanges();
            }
        }
        public void EnableProduct(int id)
        {
            var product = (from x in _db.Product
                           where x.Id == id
                           select x).FirstOrDefault();
            if (product != null)
            {
                product.IsActive = 0;
                _db.SaveChanges();
            }
        }
        public IEnumerable<ProductDTO> GetInActiveProducts() //Fyrir  admin
        {
            var products = from p in _db.Product
                          where p.IsActive == 1
                          select new ProductDTO()
                          {
                              Id = p.Id,
                              Name = p.Name,
                              IsActive = p.IsActive,
                              Views = p.ProductInfo.Views,
                              Price = p.ProductInfo.Price,
                              DiscPrice = p.ProductInfo.Price - (p.ProductInfo.Price * p.ProductInfo.Discount / 100),
                              Discount = p.ProductInfo.Discount,
                              CategoryName = p.Category.Name,
                              Description = p.ProductInfo.Description,
                              ImageUrl = p.ProductInfo.ImageUrl
                          };
            return products;
        }
    
    }
}