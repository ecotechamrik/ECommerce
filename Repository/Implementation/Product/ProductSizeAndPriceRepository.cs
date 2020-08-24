using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

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
        public string SafeGetString(DbDataReader reader, string column)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(column)))
                return reader.GetString(reader.GetOrdinal(column));
            return string.Empty;
        }

        public int SafeGetInt(DbDataReader reader, string column)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(column)))
                return reader.GetInt32(reader.GetOrdinal(column));
            return 0;
        }

        public double SafeGetDouble(DbDataReader reader, string column)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(column)))
                return reader.GetDouble(reader.GetOrdinal(column));
            return 0;
        }
        public DateTime SafeGetDate(DbDataReader reader, string column)
        {
            if (!reader.IsDBNull(reader.GetOrdinal(column)))
                return reader.GetDateTime(reader.GetOrdinal(column));
            return DateTime.Now;
        }

        public IEnumerable<ProductSizeAndPriceViewModel> GetByProductWidthID(int ProductAttributeThicknessID, int ProductWidthID)
        {
            IList<ProductSizeAndPriceViewModel> _productSizeAndPrices = new List<ProductSizeAndPriceViewModel>();
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_getProductHeightDetails";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter1 = new SqlParameter("ProductAttributeThicknessID", ProductAttributeThicknessID);
                var parameter2 = new SqlParameter("ProductWidthID", ProductWidthID);
                command.Parameters.Add(parameter1);
                command.Parameters.Add(parameter2);
                this.Context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductSizeAndPriceViewModel _productSizeAndPrice = new ProductSizeAndPriceViewModel
                        {
                            ProductSizeAndPriceID = SafeGetInt(reader, "ProductSizeAndPriceID"),                            
                            ProductAttributeThicknessID = SafeGetInt(reader, "ProductAttributeThicknessID"),
                            ProductCode = SafeGetString(reader, "ProductCode"),
                            PriceDate = SafeGetDate(reader, "PriceDate"),
                            InvDate = SafeGetDate(reader, "InvDate"),
                            RetailPriceDisc = Math.Round(SafeGetDouble(reader, "RetailPriceDisc"), 2),
                            PriceVoid = Math.Round(SafeGetDouble(reader, "PriceVoid"), 2),
                            Markup = Math.Round(SafeGetDouble(reader, "Markup"), 2),
                            SellingPrice = Math.Round(SafeGetDouble(reader, "SellingPrice"), 2),
                            CreatedDateTime = SafeGetDate(reader, "CreatedDateTime"),
                            UpdatedDateTime = SafeGetDate(reader, "UpdatedDateTime"),
                            ProductHeightID = SafeGetInt(reader, "ProductHeightID"),
                            ProductHeightName = SafeGetString(reader, "ProductHeightName"),
                            SupplierID = SafeGetInt(reader, "SupplierID"),
                            SupplierName = SafeGetString(reader, "SupplierName"),
                            InboundCost = Math.Round(SafeGetDouble(reader, "InboundCost"), 2),
                            TransportationCost = Math.Round(SafeGetDouble(reader, "TransportationCost"), 2),
                            LandedCost = Math.Round(SafeGetDouble(reader, "LandedCost"), 2)
                        };
                        _productSizeAndPrices.Add(_productSizeAndPrice);
                    }
                }
                this.Context.Database.CloseConnection();
            }

            return _productSizeAndPrices;
        }

        public IEnumerable<ProductSizeAndPriceViewModel> GetByProductAttributeID(int? ProductAttributeID)
        {
            IList<ProductSizeAndPriceViewModel> _productSizeAndPrices = new List<ProductSizeAndPriceViewModel>();
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_getByProductAttributeID";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = new SqlParameter("ProductAttributeID", ProductAttributeID);
                command.Parameters.Add(parameter);
                this.Context.Database.OpenConnection();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProductSizeAndPriceViewModel _productSizeAndPrice = new ProductSizeAndPriceViewModel
                        {
                            ProductSizeAndPriceID = reader.GetInt32(reader.GetOrdinal("ProductSizeAndPriceID")),
                            ProductAttributeID = reader.GetInt32(reader.GetOrdinal("ProductAttributeID")),
                            ProductCode = reader.GetString(reader.GetOrdinal("ProductCode")),

                            PriceDate = reader.GetDateTime(reader.GetOrdinal("PriceDate")),
                            InvDate = reader.GetDateTime(reader.GetOrdinal("InvDate")),
                            RetailPriceDisc = Math.Round(reader.GetDouble(reader.GetOrdinal("RetailPriceDisc")), 2),
                            PriceVoid = Math.Round(reader.GetDouble(reader.GetOrdinal("PriceVoid")), 2),
                            Markup = Math.Round(reader.GetDouble(reader.GetOrdinal("Markup")), 2),
                            SellingPrice = Math.Round(reader.GetDouble(reader.GetOrdinal("SellingPrice")), 2),
                            CreatedDateTime = reader.GetDateTime(reader.GetOrdinal("CreatedDateTime")),
                            UpdatedDateTime = reader.GetDateTime(reader.GetOrdinal("UpdatedDateTime")),

                            ProductHeightID = reader.GetInt32(reader.GetOrdinal("ProductHeightID")),
                            ProductHeightName = reader.GetString(reader.GetOrdinal("ProductHeightName")),

                            SupplierID = reader.GetInt32(reader.GetOrdinal("SupplierID")),
                            SupplierName = reader.GetString(reader.GetOrdinal("SupplierName")),
                            InboundCost = Math.Round(reader.GetDouble(reader.GetOrdinal("InboundCost")), 2),
                            TransportationCost = Math.Round(reader.GetDouble(reader.GetOrdinal("TransportationCost")), 2),
                            LandedCost = Math.Round(reader.GetDouble(reader.GetOrdinal("LandedCost")), 2),

                            CurrencyID = reader.GetInt32(reader.GetOrdinal("CurrencyID")),
                            CurrencyName = reader.GetString(reader.GetOrdinal("CurrencyName")),

                            MoreProductSizes = reader.GetInt32(reader.GetOrdinal("MoreProductSizes")),
                        };
                        _productSizeAndPrices.Add(_productSizeAndPrice);
                    }
                }
                this.Context.Database.CloseConnection();
            }

            return _productSizeAndPrices;
        }
    }
}