using BAL.Entities;
using BAL.ViewModels;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface ISubCatGalleryRepository : IRepository<SubCatGallery>
    {
        IEnumerable<SubCatGalleryViewModel> GetSubCatGallery();
        public void DeleteBySubCategoryID(int SubCategoryID);
        public IEnumerable<SubCatGalleryViewModel> SetDefaultImage(int SubCatGalleryID, int SubCategoryID);
    }
}
