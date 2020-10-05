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
    public class ProductSupplierRepository : Repository<ProductSupplier>, IProductSupplierRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public ProductSupplierRepository(DbContext _db) : base(_db)
        {

        }
        public IEnumerable<ProductSupplierViewModel> GetByProductSizeAndPriceID(int ProductSizeAndPriceID)
        {
            var ProductSuppliers = (from sup in Context.Suppliers

                                    join prodSup in Context.ProductSuppliers.Where(PS => PS.ProductSizeAndPriceID == ProductSizeAndPriceID)
                                    on sup.SupplierID equals prodSup.SupplierID into prodSuppliers
                                    from PSUP in prodSuppliers.DefaultIfEmpty()  // Left Outer Join of Product Widths to Product Size And Prices

                                    where sup.IsActive == true

                                    select new ProductSupplierViewModel
                                    {
                                        ProductSupplierID = PSUP.ProductSupplierID,
                                        ProductSizeAndPriceID = PSUP.ProductSizeAndPriceID,
                                        SupplierID = sup.SupplierID,
                                        SupplierName = sup.SupplierName,

                                        InboundCost = PSUP.InboundCost,
                                        LandedCost = PSUP.LandedCost,
                                        TransportationCost = PSUP.TransportationCost,
                                        IsLive = PSUP.IsLive,
                                        IsOption = PSUP.IsOption
                                    });

            return ProductSuppliers;
        }
    }
}