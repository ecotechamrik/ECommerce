using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

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
            var SubCatGallerys = (from subcatgal in Context.SubCatGalleries
                            join subcat in Context.SubCategories
                            on subcatgal.SubCategoryID equals subcat.SubCategoryID into subcatgallery
                            
                            from subcat in subcatgallery.DefaultIfEmpty()  // Left Outer Join

                            select new SubCatGalleryViewModel
                            {
                                SubCatGalleryID = subcatgal.SubCatGalleryID,
                                ThumbNailSizeImage = subcatgal.ThumbNailSizeImage,
                                MediumSizeImage = subcatgal.MediumSizeImage,
                                LargeSizeImage = subcatgal.LargeSizeImage,
                                IsMainImage = subcatgal.IsMainImage,
                                Order = subcatgal.Order,
                                SubCategoryID = subcat.SubCategoryID,
                                SubCategoryName = subcat.SubCategoryName
                            }).ToList();

            return SubCatGallerys;
        }
    }
}