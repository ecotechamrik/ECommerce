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
                                       MediumSizeImage = subcatgal.MediumSizeImage,
                                       LargeSizeImage = subcatgal.LargeSizeImage,
                                       IsMainImage = subcatgal.IsMainImage,
                                       Order = subcatgal.Order,
                                       SubCategoryID = subcat.SubCategoryID,
                                       SubCategoryName = subcat.SubCategoryName,
                                       CategoryID = cat.CategoryID,
                                       CategoryName = cat.CategoryName
                                   }).ToList();

            return SubCatGalleries;
        }

        public void DeleteBySubCategoryID(int SubCategoryID)
        {
            Context.SubCatGalleries.RemoveRange(Context.SubCatGalleries.Where(s => s.SubCategoryID == SubCategoryID));
        }

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
    }
}