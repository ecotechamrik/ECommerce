using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;

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
        /// <param name="ProductAttributeID"></param>
        /// <returns></returns>
        public IEnumerable<ProductThicknessViewModel> GetWithAttributeThicknessID(int? ProductAttributeID)
        {
            IList<ProductThicknessViewModel> _productThicknesses = new List<ProductThicknessViewModel>();
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_getWithAttributeThicknessID";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = new SqlParameter("ProductAttributeID", ProductAttributeID);
                command.Parameters.Add(parameter);
                this.Context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductThicknessViewModel _productThickness = new ProductThicknessViewModel
                        {
                            ProductThicknessID = Common.SafeGetInt(reader, "ProductThicknessID"),
                            ProductThicknessName = Common.SafeGetString(reader, "ProductThicknessName"),
                            ProductCodeInitials = Common.SafeGetString(reader, "ProductCodeInitials"),
                            ProductAttributeThicknessID = Common.SafeGetInt(reader, "ProductAttributeThicknessID"),
                            Active = Common.SafeGetBoolean(reader, "Active"),
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