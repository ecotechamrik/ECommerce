using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using DAL.DBInitializer;
using Microsoft.Data.SqlClient;
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

                            join subcat in Context.SubCategories
                            on prod.SubCategoryID equals subcat.SubCategoryID into subcatproducts
                            from subcat in subcatproducts.DefaultIfEmpty() // Left Outer Join

                            select new ProductViewModel
                            {
                                ProductID = prod.ProductID,
                                ProductName = prod.ProductName,
                                ProductDesc = prod.ProductDesc,
                                CategoryID = cat.CategoryID,
                                CategoryName = cat.CategoryName,
                                SubCategoryID = subcat.SubCategoryID,
                                SubCategoryName = subcat.SubCategoryName
                            }).ToList();

            return products;
        }

        public IEnumerable<ProductViewModel> SearchProducts(string _search)
        {
            IList<ProductViewModel> _products = new List<ProductViewModel>();
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_getProducts";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = new SqlParameter("ProductCode", _search);
                command.Parameters.Add(parameter);
                this.Context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductViewModel _product = new ProductViewModel
                        {
                            ProductID = Common.SafeGetInt(reader, "ProductID"),
                            ProductName = Common.SafeGetString(reader, "ProductName"),
                            CategoryID = Common.SafeGetInt(reader, "CategoryID"),
                            CategoryName = Common.SafeGetString(reader, "CategoryName"),
                            SubCategoryID = Common.SafeGetInt(reader, "SubCategoryID"),
                            SubCategoryName = Common.SafeGetString(reader, "SubCategoryName")                            
                        };
                        _products.Add(_product);
                    }
                }
                this.Context.Database.CloseConnection();
            }

            return _products;
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