using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class ProductThicknessRepository : Repository<ProductThickness>, IProductThicknessRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public ProductThicknessRepository(DbContext _db) : base(_db)
        {

        }

        #region [ Get Product Thickness Details with ProductAttributeThicknessID ]
        /// <summary>
        /// Get Product Thickness Details with ProductAttributeThicknessID
        /// </summary>
        /// <param name="DoorTypeID"></param>
        /// <returns></returns>
        public IEnumerable<ProductThicknessViewModel> GetWithAttributeThicknessID(int? DoorTypeID)
        {
            IList<ProductThicknessViewModel> _productThicknesses = new List<ProductThicknessViewModel>();
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_getWithAttributeThicknessID";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = new SqlParameter("DoorTypeID", DoorTypeID);
                command.Parameters.Add(parameter);
                this.Context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductThicknessViewModel _productThickness = new ProductThicknessViewModel
                        {
                            ProductThicknessID = reader.GetInt32(reader.GetOrdinal("ProductThicknessID")),
                            ProductThicknessName = reader.GetString(reader.GetOrdinal("ProductThicknessName")),
                            ProductAttributeThicknessID = reader.GetInt32(reader.GetOrdinal("ProductAttributeThicknessID")),
                        };
                        _productThicknesses.Add(_productThickness);
                    }
                }
                this.Context.Database.CloseConnection();
            }

            return _productThicknesses;
        }
        #endregion
    }
}