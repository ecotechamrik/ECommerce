#region [ Namespace ]
using BAL.Entities;
using BAL.ViewModels;
using BAL.ViewModels.Common;
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
        ISubCatGalleryAPIResponse SubCatGalleryViewRepo { get; }
        IGenerateAPIResponse<ProductViewModel> ProductViewRepo { get; }
        IGenerateAPIResponse<ProductDesignViewModel> ProductDesignViewRepo { get; }
        IGenerateAPIResponse<ProductGradeViewModel> ProductGradeViewRepo { get; }
        IGenerateAPIResponse<ProductHeightViewModel> ProductHeightViewRepo { get; }
        IGenerateAPIResponse<ProductThicknessViewModel> ProductThicknessViewRepo { get; }
        IGenerateAPIResponse<ProductWidthViewModel> ProductWidthViewRepo { get; }
        IGenerateAPIResponse<DoorTypeViewModel> DoorTypeViewRepo { get; }
        IProductAttributeAPIResponse ProductAttributeViewRepo { get; }
        IGenerateAPIResponse<ProductAttributeThicknessViewModel> ProductAttributeThicknessViewRepo { get; }
        IGenerateAPIResponse<SupplierViewModel> SupplierViewRepo { get; }
        IProductSizeAndPriceAPIResponse ProductSizeAndPriceViewRepo { get; }
        #endregion

        #region [ Website Info ]
        IGenerateAPIResponse<WebsiteInfoViewModel> WebsiteInfoViewRepo { get; }
        #endregion

        #region [ Common ]
        IGenerateAPIResponse<String> CommonRepo { get; }
        IGenerateAPIResponse<CurrencyViewModel> CurrencyViewRepo { get; }
        #endregion
    }
}