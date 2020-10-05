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
    public class ProductImagesRepository : Repository<ProductImage>, IProductImagesRepository
    {
        #region [ DB Context and Default Constructor ]
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public ProductImagesRepository(DbContext _db) : base(_db)
        {

        }
        #endregion

        #region [ Get Product Images with Join of Product ]
        /// <summary>
        /// Get Product Images with Join of Product
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductImageViewModel> GetProductImages()
        {
            var productImages = (from prodimg in Context.ProductImages

                                   join prod in Context.Products
                                   on prodimg.ProductID equals prod.ProductID into producimages
                                   from prod in producimages.DefaultIfEmpty()  // Left Outer Join

                                   select new ProductImageViewModel
                                   {
                                       ProductImageID = prodimg.ProductImageID,
                                       ThumbNailSizeImage = prodimg.ThumbNailSizeImage,
                                       OriginalImage = prodimg.OriginalImage,
                                       IsMainImage = prodimg.IsMainImage,
                                       Order = prodimg.Order,
                                       ProductID = prod.ProductID,
                                       ProductName = prod.ProductName
                                   }).ToList();

            return productImages;
        }
        #endregion

        #region [ Delete All Product Images by ProductID ]
        /// <summary>
        /// Delete All Product Images by ProductID
        /// </summary>
        /// <param name="ProductID"></param>
        public void DeleteImagesByProductID(int ProductID)
        {
            Context.ProductImages.RemoveRange(Context.ProductImages.Where(s => s.ProductID == ProductID));
        }
        #endregion

        #region [ Update Image Order ]
        /// <summary>
        /// Update Image Order
        /// </summary>
        /// <param name="ProductImageID"></param>
        /// <param name="OrderNo"></param>
        public void UpdateOrder(int ProductImageID, int OrderNo)
        {
            var productImage = (from prodimg in Context.ProductImages
                                 where prodimg.ProductImageID == ProductImageID
                                 select prodimg).FirstOrDefault();
            productImage.Order = OrderNo;
            Context.Update(productImage);
        }
        #endregion

        #region [ Set Default Image ]
        /// <summary>
        /// Set Default Image
        /// </summary>
        /// <param name="ProductImageID"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public IEnumerable<ProductImageViewModel> SetDefaultImage(int ProductImageID, int ProductID)
        {
            var productImages = (from prodimg in Context.ProductImages
                                   where prodimg.ProductID == ProductID
                                   select prodimg).ToList();

            productImages.ForEach(prodimg => prodimg.IsMainImage = false);
            productImages.Where(s => s.ProductImageID == ProductImageID).FirstOrDefault().IsMainImage = true;

            Context.SaveChanges();

            return GetProductImages().Where(s => s.ProductID == ProductID);
        }
        #endregion
    }
}