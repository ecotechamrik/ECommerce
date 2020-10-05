using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

        #region [ Update Price Void ]
        /// <summary>
        /// Update Image Order
        /// </summary>
        /// <param name="ProductSizeAndPriceID"></param>
        /// <param name="PriceVoid"></param>
        public void UpdatePriceVoid(int ProductSizeAndPriceID, double PriceVoid)
        {
            var productSizeAndPrice = (from prodprisize in Context.ProductSizeAndPrices
                                where prodprisize.ProductSizeAndPriceID == ProductSizeAndPriceID
                                       select prodprisize).FirstOrDefault();
            productSizeAndPrice.PriceVoid = PriceVoid;
            Context.Update(productSizeAndPrice);
        }
        #endregion
        public IEnumerable<ProductSizeAndPriceViewModel> GetByProductWidthID(int ProductAttributeThicknessID, int ProductWidthID)
        {
            var ProductSizeAndPrices = (from prodheights in Context.ProductHeights

                                        join prodSizePrices in Context.ProductSizeAndPrices.Where(PSP => PSP.ProductAttributeThicknessID == ProductAttributeThicknessID && PSP.ProductWidthID == ProductWidthID)
                                        on prodheights.ProductHeightID equals prodSizePrices.ProductHeightID into prodAttributes
                                        from PSP in prodAttributes.DefaultIfEmpty()  // Left Outer Join of Product Heights to Product Size And Prices

                                        select new ProductSizeAndPriceViewModel
                                        {
                                            ProductSizeAndPriceID = PSP.ProductSizeAndPriceID,
                                            ProductCode = PSP.ProductCode,
                                            Description = PSP.Description,
                                            ProductAttributeThicknessID = PSP.ProductAttributeThicknessID,
                                            ProductHeightID = prodheights.ProductHeightID,
                                            ProductHeightName = prodheights.ProductHeightName,
                                            ProductWidthID = ProductWidthID,
                                            PriceDate = PSP.PriceDate,
                                            InvDate = PSP.InvDate,
                                            RetailPriceDisc = PSP.RetailPriceDisc,
                                            PriceVoid = PSP.PriceVoid,
                                            Markup = PSP.Markup,
                                            SellingPrice = PSP.SellingPrice,
                                            CreatedDateTime = PSP.CreatedDateTime,
                                            UpdatedDateTime = PSP.UpdatedDateTime,

                                            PriorityNumber = PSP.PriorityNumber,
                                            InventoryNumber = PSP.InventoryNumber,
                                            Notes = PSP.Notes,
                                            GroupNumber = PSP.GroupNumber,
                                            BuildingCode = PSP.BuildingCode,
                                            LocationCode = PSP.LocationCode,
                                            InventoryLevel = PSP.InventoryLevel,
                                            LeadTime = PSP.LeadTime,
                                            BestQuantityNo = PSP.BestQuantityNo,
                                            OrderNowNo = PSP.OrderNowNo,
                                            RetailBin = PSP.RetailBin,
                                            WholeSaleBin = PSP.WholeSaleBin,
                                            IndexNumber = PSP.IndexNumber
                                        });

            return ProductSizeAndPrices;
        }

        #region [ Get Product Attribute Details by ProductAttributeID ]
        /// <summary>
        /// Get Product Attribute Details by ProductAttributeID
        /// </summary>
        /// <param name="ProductAttributeID"></param>
        /// <returns></returns>
        public IEnumerable<ProductSizeAndPriceViewModel> ProductAttributeDetails(int ProductAttributeID)
        {
            IList<ProductSizeAndPriceViewModel> _productSizeAndPrices = new List<ProductSizeAndPriceViewModel>();
            using (var command = Context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "sp_getProductAttributeDetails";
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
                            Description = Common.SafeGetString(reader, "Description"),
                            ProductThicknessName = Common.SafeGetString(reader, "ProductThicknessName"),
                            Column80 = Common.SafeGetString(reader, "80"),
                            Column84 = Common.SafeGetString(reader, "84"),
                            Column90 = Common.SafeGetString(reader, "90"),
                            Column96 = Common.SafeGetString(reader, "96"),
                            Column108 = Common.SafeGetString(reader, "108"),                            
                        };
                        _productSizeAndPrices.Add(_productSizeAndPrice);
                    }
                }
                this.Context.Database.CloseConnection();
            }

            return _productSizeAndPrices;
        }
        #endregion
    }
}