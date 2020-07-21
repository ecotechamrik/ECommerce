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

        private IUserRepository _userRepo;
        public IUserRepository UserRepo
        {
            get
            {
                if (_userRepo == null)
                    _userRepo = new UserRepository(db);
                return _userRepo;
            }
        }

        private IRepository<Section> _sectionRepo;
        public IRepository<Section> SectionRepo
        {
            get
            {
                if (_sectionRepo == null)
                    _sectionRepo = new Repository<Section>(db);
                return _sectionRepo;
            }
        }

        private ICategoryRepository _categoryRepo;
        public ICategoryRepository CategoryRepo
        {
            get
            {
                if (_categoryRepo == null)
                    _categoryRepo = new CategoryRepository(db);
                return _categoryRepo;
            }
        }

        private ISubCategoryRepository _subCategoryRepo;
        public ISubCategoryRepository SubCategoryRepo
        {
            get
            {
                if (_subCategoryRepo == null)
                    _subCategoryRepo = new SubCategoryRepository(db);
                return _subCategoryRepo;
            }
        }

        private ISubCatGalleryRepository _subCatGalleryRepo;
        public ISubCatGalleryRepository SubCatGalleryRepo
        {
            get
            {
                if (_subCatGalleryRepo == null)
                    _subCatGalleryRepo = new SubCatGalleryRepository(db);
                return _subCatGalleryRepo;
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
