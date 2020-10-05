using BAL.Entities;
using Repository.Abstraction;

namespace Repository
{
    public interface IUnitOfWork
    {
        #region [ Common Repositories ]
        /// <summary>
        /// Common Repositories
        /// </summary>
        IRepository<Currency> CurrencyRepo { get; }
        #endregion

        #region [ Product Related Repositories ]
        /// <summary>
        /// Product Related Repositories
        /// </summary>

        IRepository<Section> SectionRepo { get; }        
        ICategoryRepository CategoryRepo { get; }
        ISubCategoryRepository SubCategoryRepo { get; }
        ISubCatGalleryRepository SubCatGalleryRepo { get; }
        IProductRepository ProductRepo { get; }        
        IRepository<ProductDesign> ProductDesignRepo { get; }
        IRepository<ProductHeight> ProductHeightRepo { get; }
        IRepository<ProductWidth> ProductWidthRepo { get; }
        IProductThicknessRepository ProductThicknessRepo { get; }
        IProductSupplierRepository ProductSupplierRepo { get; }
        IRepository<ProductGrade> ProductGradeRepo { get; }
        IRepository<DoorType> DoorTypeRepo { get; }
        IProductAttributeRepository ProductAttributeRepo { get; }
        IRepository<Supplier> SupplierRepo { get; }
        IRepository<ProductAttributeThickness> ProductAttributeThicknessRepo { get; }
        IProductSizeAndPriceRepository ProductSizeAndPriceRepo { get; }
        IProductImagesRepository ProductImagesRepo { get; }
        #endregion

        #region [ Website Related Repositories ]
        /// <summary>
        /// Website Related Repositories
        /// </summary>
        IWebsiteInfoRepository WebsiteInfoRepo { get; }
        #endregion

        #region [ User Related Repositories ]
        /// <summary>
        /// User Related Repositories
        /// </summary>
        IUserRepository UserRepo { get; }
        #endregion
        int SaveChanges();
    }
}
