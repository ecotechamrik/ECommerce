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
