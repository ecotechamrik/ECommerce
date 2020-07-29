using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using DAL.DBInitializer;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public ProductRepository(DbContext _db) : base(_db)
        {

        }
        public IEnumerable<ProductViewModel> GetProductsWithCategories()
        {
            var products = (from prod in Context.Products
                            join cat in Context.Categories
                            on prod.CategoryID equals cat.CategoryID into catproducts
                            
                            from cat in catproducts.DefaultIfEmpty() // Left Outer Join

                            select new ProductViewModel
                            {
                                ProductID = prod.ProductID,
                                ProductName = prod.ProductName,
                                ProductCode = prod.ProductCode,
                                ProductDesc = prod.ProductDesc,
                                CategoryID = cat.CategoryID,
                                CategoryName = cat.CategoryName
                            }).ToList();

            return products;
        }

        /// <summary>
        /// Initialize DB entries at the very first time if DB is blank.
        /// </summary>
        public void DbInitialize()
        {
            ProductDbInitializer.Initialize(Context);
        }
    }
}