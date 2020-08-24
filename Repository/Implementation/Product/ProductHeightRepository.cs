using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class ProductHeightRepository : Repository<ProductHeight>, IProductHeightRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public ProductHeightRepository(DbContext _db) : base(_db)
        {

        }

        #region [ Get Product Heights Except already added into Product Size and Prices - For Edit Mode ]
        /// <summary>
        /// [ Get Product Heights Except already added into Product Size and Prices - For Edit Mode ]
        /// </summary>
        /// <param name="ProductAttrID"></param>
        /// <returns></returns>
        public IEnumerable<ProductHeightViewModel> GetProductHeightNotAdded(int? ProductAttrID, int? ProductSizeAndPriceID)
        {
            IList<ProductHeightViewModel> _productSizeAndPrices = new List<ProductHeightViewModel>();
            var ProductHeights = (from prodHeight in Context.ProductHeights
                                  where !Context.ProductSizeAndPrices.Any(p => p.ProductHeightID == prodHeight.ProductHeightID
                                                                                && p.ProductAttributeThicknessID == ProductAttrID
                                                                                && p.ProductSizeAndPriceID != ProductSizeAndPriceID)

                                        select new ProductHeightViewModel
                                        {
                                            ProductHeightID = prodHeight.ProductHeightID,
                                            ProductHeightName = prodHeight.ProductHeightName,
                                        }).ToList();

            return ProductHeights;
        }
        #endregion
    }
}