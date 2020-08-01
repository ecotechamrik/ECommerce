using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Repository.Implementation
{
    public class SubCatGalleryRepository : Repository<SubCatGallery>, ISubCatGalleryRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public SubCatGalleryRepository(DbContext _db) : base(_db)
        {

        }

        #region [ Get Sub Category Galleries with Join of Category ]
        /// <summary>
        /// Get Sub Category Galleries with Join of Category
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SubCatGalleryViewModel> GetSubCatGallery()
        {
            var SubCatGalleries = (from subcatgal in Context.SubCatGalleries
                                   join subcat in Context.SubCategories
                                   on subcatgal.SubCategoryID equals subcat.SubCategoryID into subcatgallery
                                   from subcat in subcatgallery.DefaultIfEmpty()  // Left Outer Join

                                   join cat in Context.Categories
                                   on subcat.CategoryID equals cat.CategoryID into subcategories
                                   from cat in subcategories.DefaultIfEmpty()  // Left Outer Join

                                   select new SubCatGalleryViewModel
                                   {
                                       SubCatGalleryID = subcatgal.SubCatGalleryID,
                                       ThumbNailSizeImage = subcatgal.ThumbNailSizeImage,
                                       OriginalImage = subcatgal.OriginalImage,
                                       IsMainImage = subcatgal.IsMainImage,
                                       Order = subcatgal.Order,
                                       SubCategoryID = subcat.SubCategoryID,
                                       SubCategoryName = subcat.SubCategoryName,
                                       CategoryID = cat.CategoryID,
                                       CategoryName = cat.CategoryName
                                   }).ToList();

            return SubCatGalleries;
        }
        #endregion

        #region [ Delete All Sub Cat Galleries by SubCategoryID ]
        /// <summary>
        /// Delete All Sub Cat Galleries by SubCategoryID
        /// </summary>
        /// <param name="SubCategoryID"></param>
        public void DeleteBySubCategoryID(int SubCategoryID)
        {
            Context.SubCatGalleries.RemoveRange(Context.SubCatGalleries.Where(s => s.SubCategoryID == SubCategoryID));
        }
        #endregion

        #region [ Update Image Order ]
        /// <summary>
        /// Update Image Order
        /// </summary>
        /// <param name="SubCatGalleryID"></param>
        /// <param name="OrderNo"></param>
        public void UpdateOrder(int SubCatGalleryID, int OrderNo)
        {
            var subcatGallery = (from subcatgal in Context.SubCatGalleries
                                   where subcatgal.SubCatGalleryID == SubCatGalleryID
                                   select subcatgal).FirstOrDefault();
            subcatGallery.Order = OrderNo;
            Context.Update(subcatGallery);
        }
        #endregion

        #region [ Set Default Image ]
        /// <summary>
        /// Set Default Image
        /// </summary>
        /// <param name="SubCatGalleryID"></param>
        /// <param name="SubCategoryID"></param>
        /// <returns></returns>
        public IEnumerable<SubCatGalleryViewModel> SetDefaultImage(int SubCatGalleryID, int SubCategoryID)
        {
            var subcatGalleries = (from subcatgal in Context.SubCatGalleries
                                   where subcatgal.SubCategoryID == SubCategoryID
                                   select subcatgal).ToList();

            subcatGalleries.ForEach(subcatgal => subcatgal.IsMainImage = false);
            subcatGalleries.Where(s => s.SubCatGalleryID == SubCatGalleryID).FirstOrDefault().IsMainImage = true;
            
            Context.SaveChanges();
            
            return GetSubCatGallery().Where(s => s.SubCategoryID == SubCategoryID);
        }
        #endregion
    }
}