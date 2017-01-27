using System.Collections.Generic;
using System.Linq;
using BlankAPI.Models.EF;
using BlankAPI.Models.DTO;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BlankAPI.Queries
{
    public class ProductQueries
    {
        private blankdbEntities _db;
        public ProductQueries()
        {
            _db = new blankdbEntities();
        }
        public IEnumerable<CategoryDTO> GetCat() //virkar /getCategories  / tshirt - hoodies - misc
        {
            var categories = from c in _db.Category
                             select new CategoryDTO()
                             {
                                 Id = c.Id,
                                 Name = c.Name,
                                 About = c.About,
                                 ImageUrl = c.ImageUrl
                             };
            return categories;
        }
        public IEnumerable<ProductDTO> GetAllForSearch()
        {
            var product = from p in _db.Product
                          where p.IsActive == 0
                          select new ProductDTO()
                          {
                              Id = p.Id,
                              Name = p.Name
                          };
            return product;
        }
        public IEnumerable<ProductDTO> GetPopular()    //get popular / skilar 9 most viewed products
        {
            var product = from p in _db.Product
                          where p.IsActive == 0
                          select new ProductDTO()
                          {
                              Id = p.Id,
                              Name = p.Name,
                              CategoryName = p.Category.Name,
                              Price = p.ProductInfo.Price,
                              DiscPrice = p.ProductInfo.Price - (p.ProductInfo.Price * p.ProductInfo.Discount / 100),
                              Discount = p.ProductInfo.Discount,
                              Views = p.ProductInfo.Views,
                              ImageUrl = p.ProductInfo.ImageUrl
                          };
            var sorted = product.OrderByDescending(item => item.Views);
            return sorted.Take(9);
        }
        public IEnumerable<ProductDTO> GetOnSale()    //get products on sale
        {
            var product = from p in _db.Product
                          where p.ProductInfo.Discount > 0 & p.IsActive == 0
                          select new ProductDTO()
                          {
                              Id = p.Id,
                              Name = p.Name,
                              CategoryName = p.Category.Name,
                              Price = p.ProductInfo.Price,
                              DiscPrice = p.ProductInfo.Price - (p.ProductInfo.Price * p.ProductInfo.Discount / 100),
                              Discount = p.ProductInfo.Discount,
                              ImageUrl = p.ProductInfo.ImageUrl
                          };
            return product;
        }
        public IEnumerable<ProductDTO> GetList(string categoryName) //virkar /grid/{categoryName}
        {
            var list = from p in _db.Product
                       where p.Category.Name == categoryName & p.IsActive == 0
                       select new ProductDTO()
                       {
                           Id = p.Id,
                           Name = p.Name,
                           Price = p.ProductInfo.Price,
                           DiscPrice = p.ProductInfo.Price - (p.ProductInfo.Price * p.ProductInfo.Discount / 100),
                           Discount = p.ProductInfo.Discount,
                           ImageUrl = p.ProductInfo.ImageUrl
                       };
            return list;
        }
        public static List<int> oldIdList = new List<int> { 0, 0 };
        public ProductDTO GetById(int id) //virkar get /item/{id}
        {
            oldIdList.Add(id); //semi fix if refresh eða beint revisit //uppá comment eða rating refresh
            oldIdList.RemoveAt(0); 
            int oldId = oldIdList[0];
            if (oldId != id)
            {
                var prod = (from x in _db.Product //adda View á product
                               where x.Id == id
                               select x).FirstOrDefault();
                prod.ProductInfo.Views++;
                _db.SaveChanges(); 
            }
            var ratingsData = from r in _db.Ratings //ráða úr ratings
                         where r.ProductId == id
                         select new RatingDTO() {
                             Rating = r.Rating
                         };
            double votes = 0;
            double total = 0;
            foreach(RatingDTO x in ratingsData)
            {
                votes++;
                total += x.Rating;
            }
            double calcRating = 0;
            if (votes != 0 && total != 0)
            {
                calcRating = total / votes;
            }

            var product = from p in _db.Product //sækja product
                          where p.Id == id
                          select new ProductDTO()
                          {
                              Id = p.Id,
                              Name = p.Name,
                              IsActive = p.IsActive,
                              CategoryName = p.Category.Name,
                              CategoryId = p.Category.Id,
                              Price = p.ProductInfo.Price,
                              DiscPrice = p.ProductInfo.Price - (p.ProductInfo.Price * p.ProductInfo.Discount / 100),
                              AvgRating = calcRating,
                              RatingVotes = votes,
                              Quantity = 1,
                              Discount = p.ProductInfo.Discount,
                              Description = p.ProductInfo.Description,
                              ImageUrl = p.ProductInfo.ImageUrl
                          };
            return product.FirstOrDefault();
        }
    }
}