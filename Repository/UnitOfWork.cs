using BAL.Entities;
using DAL;
using Repository.Abstraction;
using Repository.Implementation;

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        protected DatabaseContext db;

        public UnitOfWork()
        {
            // Get Connection String from DBContextHelper class
            //db = new DatabaseContext(DbContextHelper.GetDbContextOptions());
            db = new DatabaseContext();
        }

        private IRepository<Category> _categoryRepo;
        public IRepository<Category> CategoryRepo 
        {
            get 
            {
                if (_categoryRepo == null)
                    _categoryRepo = new Repository<Category>(db);
                return _categoryRepo;
            }
        }

        private IWebsiteInfoRepository _websiteInfoRepo;
        public IWebsiteInfoRepository WebsiteInfoRepo
        {
            get
            {
                if (_websiteInfoRepo == null)
                    _websiteInfoRepo = new WebsiteInfoRepository(db);
                return _websiteInfoRepo;
            }
        }

        private IProductRepository _productRepo;
        public IProductRepository ProductRepo
        {
            get
            {
                if (_productRepo == null)
                    _productRepo = new ProductRepository(db);
                return _productRepo;
            }
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}
