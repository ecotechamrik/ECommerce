using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class ProductAttributeRepository : Repository<ProductAttribute>, IProductAttributeRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public ProductAttributeRepository(DbContext _db) : base(_db)
        {

        }

        public IEnumerable<ProductAttributeViewModel> GetAttributesByID(int? id)
        {
            var ProductAttributes = (from prodAttr in Context.ProductAttributes
                                     where prodAttr.ProductAttributeID == id

                                     join doorType in Context.DoorType
                                     on prodAttr.DoorTypeID equals doorType.DoorTypeID into prodAttrDoroType
                                     from doorType in prodAttrDoroType.DefaultIfEmpty()  // Left Outer Join of Product Attributes to Door Types

                                     join currency in Context.Currencies
                                     on prodAttr.CurrencyID equals currency.CurrencyID into prodAttrCurrency
                                     from currency in prodAttrCurrency.DefaultIfEmpty()  // Left Outer Join of Product Attributes to Currency

                                     select new ProductAttributeViewModel
                                     {
                                         ProductAttributeID = prodAttr.ProductAttributeID,
                                         ProductAttributeName = doorType.DoorTypeName,
                                         Description = prodAttr.Description,
                                         ProductID = prodAttr.ProductID,
                                         CurrencyID = currency.CurrencyID,
                                         CurrencyName = currency.CurrencyName
                                     }).ToList();

            return ProductAttributes;
        }

        public IEnumerable<ProductAttributeViewModel> GetAttributesByProductID(int? id)
        {
            var ProductAttributes = (from prodAttr in Context.ProductAttributes
                                     where prodAttr.ProductID == id

                                     join doorType in Context.DoorType
                                     on prodAttr.DoorTypeID equals doorType.DoorTypeID into prodAttrDoroType
                                     from doorType in prodAttrDoroType.DefaultIfEmpty()  // Left Outer Join of Product Attributes to Door Types

                                     join currency in Context.Currencies
                                     on prodAttr.CurrencyID equals currency.CurrencyID into prodAttrCurrency
                                     from currency in prodAttrCurrency.DefaultIfEmpty()  // Left Outer Join of Product Attributes to Currency

                                     select new ProductAttributeViewModel
                                     {
                                         ProductAttributeID = prodAttr.ProductAttributeID,
                                         ProductAttributeName = doorType.DoorTypeName,
                                         Description = prodAttr.Description,
                                         ProductID = prodAttr.ProductID,
                                         CurrencyID = currency.CurrencyID,
                                         CurrencyName = currency.CurrencyName

                                     }).ToList();

            return ProductAttributes;
        }

        public void UpdateProductAttribute(ProductAttribute model)
        {
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_UpdateProductAttribute";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter[] _parameters =
                {
                  new SqlParameter("ProductAttributeID", model.ProductAttributeID),
                  new SqlParameter("CurrencyID", model.CurrencyID),
                  new SqlParameter("ProductAttributeName", model.ProductAttributeName),
                  new SqlParameter("Description", model.Description),
                  new SqlParameter("DoorTypeID", model.DoorTypeID),
                  new SqlParameter("ProductActiveAttributes", model.ProductActiveAttributes)
                };

                command.Parameters.AddRange(_parameters);
                Context.Database.OpenConnection();
                command.ExecuteNonQuery();
                Context.Database.CloseConnection();
            }
        }
    }
}