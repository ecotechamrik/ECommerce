using BAL.Entities;
using Repository.Abstraction;

namespace Repository
{
    public interface IUnitOfWork
    {
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
        IRepository<ProductSize> ProductSizeRepo { get; }
        IRepository<ProductGrade> ProductGradeRepo { get; }
        IRepository<DoorType> DoorTypeRepo { get; }
        IRepository<Supplier> SupplierRepo { get; }
        IProductSizeAndPriceRepository ProductSizeAndPriceRepo { get; }
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
