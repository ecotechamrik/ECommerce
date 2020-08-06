#region [ Namespace ]
using BAL.Entities;
using DAL;
using Repository.Abstraction;
using Repository.Implementation;
#endregion

namespace Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region [ Local Variables ]
        /// <summary>
        /// Local Variables
        /// </summary>
        protected DatabaseContext db;
        #endregion

        #region [ Default Constructor ]
        /// <summary>
        /// Default Constructor
        /// </summary>
        public UnitOfWork()
        {
            // Get Connection String from DBContextHelper class
            //db = new DatabaseContext(DbContextHelper.GetDbContextOptions());
            db = new DatabaseContext();
        }
        #endregion

        #region [ User Repository ]
        /// <summary>
        /// User Repository
        /// </summary>
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
        #endregion

        #region [ Website Info Repository ]
        /// <summary>
        /// Website Info Repository
        /// </summary>
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
        #endregion

        #region [ Section Repository ]
        /// <summary>
        /// Section Repository
        /// </summary>
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
        #endregion

        #region [ Category Repository ]
        /// <summary>
        /// Category Repository
        /// </summary>
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
        #endregion

        #region [ Sub Category Repository ]
        /// <summary>
        /// Sub Category Repository
        /// </summary>
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
        #endregion

        #region [ Sub Category Gallery Repository ]
        /// <summary>
        /// Sub Category Gallery Repository
        /// </summary>
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
        #endregion        

        #region [ Product Repository ]
        /// <summary>
        /// Product Repository
        /// </summary>
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
        #endregion

        #region [ Product Design Repository ]
        /// <summary>
        /// Product Design Repository
        /// </summary>
        private IRepository<ProductDesign> _productDesignRepo;
        public IRepository<ProductDesign> ProductDesignRepo
        {
            get
            {
                if (_productDesignRepo == null)
                    _productDesignRepo = new Repository<ProductDesign>(db);
                return _productDesignRepo;
            }
        }
        #endregion        

        #region [ Product Grade Repository ]
        /// <summary>
        /// Product Grade Repository
        /// </summary>
        private IRepository<ProductGrade> _productGradeRepo;
        public IRepository<ProductGrade> ProductGradeRepo
        {
            get
            {
                if (_productGradeRepo == null)
                    _productGradeRepo = new Repository<ProductGrade>(db);
                return _productGradeRepo;
            }
        }
        #endregion

        #region [ Door Type Repository ]
        /// <summary>
        /// Door Type Repository
        /// </summary>
        private IRepository<DoorType> _doorTypeRepo;
        public IRepository<DoorType> DoorTypeRepo
        {
            get
            {
                if (_doorTypeRepo == null)
                    _doorTypeRepo = new Repository<DoorType>(db);
                return _doorTypeRepo;
            }
        }
        #endregion

        #region [ Product Size Repository ]
        /// <summary>
        /// Product Size Repository
        /// </summary>
        private IRepository<ProductSize> _productSizeRepo;
        public IRepository<ProductSize> ProductSizeRepo
        {
            get
            {
                if (_productSizeRepo == null)
                    _productSizeRepo = new Repository<ProductSize>(db);
                return _productSizeRepo;
            }
        }
        #endregion

        #region [ Supplier Repository ]
        /// <summary>
        /// Supplier Repository
        /// </summary>
        private IRepository<Supplier> _supplierRepo;
        public IRepository<Supplier> SupplierRepo
        {
            get
            {
                if (_supplierRepo == null)
                    _supplierRepo = new Repository<Supplier>(db);
                return _supplierRepo;
            }
        }
        #endregion

        #region [ Product Size And Price Repository ]
        /// <summary>
        /// Product Size And Prices Repository
        /// </summary>
        private IProductSizeAndPriceRepository _productSizeAndPriceRepo;
        public IProductSizeAndPriceRepository ProductSizeAndPriceRepo
        {
            get
            {
                if (_productSizeAndPriceRepo == null)
                    _productSizeAndPriceRepo = new ProductSizeAndPriceRepository(db);
                return _productSizeAndPriceRepo;
            }
        }
        #endregion

        #region [ Final Save Changes ]
        /// <summary>
        /// Final Save Changes
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        #endregion
    }
}
