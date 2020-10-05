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

        #region [ Currency Repository ]
        /// <summary>
        /// Currency Repository
        /// </summary>
        private IRepository<Currency> _currencyRepo;
        public IRepository<Currency> CurrencyRepo
        {
            get
            {
                if (_currencyRepo == null)
                    _currencyRepo = new Repository<Currency>(db);
                return _currencyRepo;
            }
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

        #region [ Product Attribute Repository ]
        /// <summary>
        /// Product Attribute Repository
        /// </summary>
        private IProductAttributeRepository _productAttributeRepo;
        public IProductAttributeRepository ProductAttributeRepo
        {
            get
            {
                if (_productAttributeRepo == null)
                    _productAttributeRepo = new ProductAttributeRepository(db);
                return _productAttributeRepo;
            }
        }
        #endregion

        #region [ Product Height Repository ]
        /// <summary>
        /// Product Height Repository
        /// </summary>
        private IRepository<ProductHeight> _productHeightRepo;
        public IRepository<ProductHeight> ProductHeightRepo
        {
            get
            {
                if (_productHeightRepo == null)
                    _productHeightRepo = new Repository<ProductHeight>(db);
                return _productHeightRepo;
            }
        }
        #endregion        

        #region [ Product Width Repository ]
        /// <summary>
        /// Product Width Repository
        /// </summary>
        private IRepository<ProductWidth> _productWidthRepo;
        public IRepository<ProductWidth> ProductWidthRepo
        {
            get
            {
                if (_productWidthRepo == null)
                    _productWidthRepo = new Repository<ProductWidth>(db);
                return _productWidthRepo;
            }
        }
        #endregion        

        #region [ Product Thickness Repository ]
        /// <summary>
        /// Product Thickness Repository
        /// </summary>
        private IProductThicknessRepository _productThicknessRepo;
        public IProductThicknessRepository ProductThicknessRepo
        {
            get
            {
                if (_productThicknessRepo == null)
                    _productThicknessRepo = new ProductThicknessRepository(db);
                return _productThicknessRepo;
            }
        }
        #endregion

        #region [ Product Supplier Repository ]
        /// <summary>
        /// Product Supplier Repository
        /// </summary>
        private IProductSupplierRepository _productSupplierRepo;
        public IProductSupplierRepository ProductSupplierRepo
        {
            get
            {
                if (_productSupplierRepo == null)
                    _productSupplierRepo = new ProductSupplierRepository(db);
                return _productSupplierRepo;
            }
        }
        #endregion

        #region [ Product Attribute Thickness Repository ]
        /// <summary>
        /// Supplier Repository
        /// </summary>
        private IRepository<ProductAttributeThickness> _productAttributeThicknessRepo;
        public IRepository<ProductAttributeThickness> ProductAttributeThicknessRepo
        {
            get
            {
                if (_productAttributeThicknessRepo == null)
                    _productAttributeThicknessRepo = new Repository<ProductAttributeThickness>(db);
                return _productAttributeThicknessRepo;
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

        #region [ Product Images Repository ]
        /// <summary>
        /// Product Images Repository
        /// </summary>
        private IProductImagesRepository _productImagesRepo;
        public IProductImagesRepository ProductImagesRepo
        {
            get
            {
                if (_productImagesRepo == null)
                    _productImagesRepo = new ProductImagesRepository(db);
                return _productImagesRepo;
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
