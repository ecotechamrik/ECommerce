#region [ Namespace ]
using BAL.ViewModels;
using BAL.ViewModels.Product;
using System;
#endregion

namespace BAL
{
    public interface IUnitOfWork
    {
        #region [ Section, Category, Product ]
        IGenerateAPIResponse<SectionViewModel> SectionViewRepo { get; }
        IGenerateAPIResponse<CategoryViewModel> CategoryViewRepo { get; }
        IGenerateAPIResponse<SubCategoryViewModel> SubCategoryViewRepo { get; }
        IGenerateAPIResponse<SubCatGalleryViewModel> SubCatGalleryViewRepo { get; }
        IGenerateAPIResponse<ProductViewModel> ProductViewRepo { get; }
        IGenerateAPIResponse<DoorTypeViewModel> DoorTypeViewRepo { get; }
        IGenerateAPIResponse<ProductDesignViewModel> ProductDesignViewRepo { get; }
        IGenerateAPIResponse<ProductGradeViewModel> ProductGradeViewRepo { get; }
        #endregion

        #region [ Website Info ]
        IGenerateAPIResponse<WebsiteInfoViewModel> WebsiteInfoViewRepo { get; }
        #endregion

        #region [ Common ]
        IGenerateAPIResponse<String> CommonRepo { get; }
        #endregion
    }
}