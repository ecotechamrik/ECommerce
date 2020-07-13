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
        IRepository<Category> CategoryRepo { get; }
        IProductRepository ProductRepo { get; }
        #endregion

        #region [ Website Related Repositories ]
        /// <summary>
        /// Website Related Repositories
        /// </summary>
        IWebsiteInfoRepository WebsiteInfoRepo { get; }
        #endregion

        int SaveChanges();
    }
}
