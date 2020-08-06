using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using DAL.DBInitializer;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Text;

namespace Repository.Implementation
{
    public class ProductSizeAndPriceRepository : Repository<ProductSizeAndPrice>, IProductSizeAndPriceRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public ProductSizeAndPriceRepository(DbContext _db) : base(_db)
        {

        }
        public IEnumerable<ProductWithAttrViewModel> GetProdAttrWithPrices(int ProductID)
        {
            IList<ProductWithAttrViewModel> _prods = new List<ProductWithAttrViewModel>();
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "usp_getProduct";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = new SqlParameter("ProductId", ProductID);
                command.Parameters.Add(parameter);
                this.Context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductWithAttrViewModel _prod = new ProductWithAttrViewModel
                        { 
                            ProductID = reader.GetInt32(reader.GetOrdinal("ProductId")), 
                            ProductName = reader.GetString(reader.GetOrdinal("ProductName")),
                            ProductDesc = reader.GetString(reader.GetOrdinal("ProductDesc")),
                            CategoryID = reader.GetInt32(reader.GetOrdinal("CategoryID")),

                            Height80 = reader.GetString(reader.GetOrdinal("80\"")),
                            Height84 = reader.GetString(reader.GetOrdinal("80\"")),
                            Height90 = reader.GetString(reader.GetOrdinal("90\"")),
                            Height96 = reader.GetString(reader.GetOrdinal("96\"")),
                            BestQuantity = reader.GetInt32(reader.GetOrdinal("BestQuantity"))
                        };
                        _prods.Add(_prod);
                    }
                }
                this.Context.Database.CloseConnection();
            }

            return _prods;
        }
    }
}