using BAL.ViewModels.Product;
using Microsoft.Extensions.Configuration;

namespace BAL
{
    public class UnitOfWork : IUnitOfWork
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        IConfiguration config;
        #endregion

        #region [ Default Constructor  ]
        public UnitOfWork(IConfiguration _config)
        {
            config = _config;
        }
        #endregion

        #region [ Category View Repository ]
        /// <summary>
        /// Category View Repository
        /// </summary>
        private IGenerateAPIResponse<CategoryViewModel> _categoryViewRepo;
        public IGenerateAPIResponse<CategoryViewModel> CategoryViewRepo
        {
            get
            {
                if (_categoryViewRepo == null)
                    _categoryViewRepo = new GenerateAPIResponse<CategoryViewModel>(config);
                return _categoryViewRepo;
            }
        }
        #endregion

        #region [ Sub Category View Repository ]
        /// <summary>
        /// Sub Category View Repository
        /// </summary>
        private IGenerateAPIResponse<SubCategoryViewModel> _subCategoryViewRepo;
        public IGenerateAPIResponse<SubCategoryViewModel> SubCategoryViewRepo
        {
            get
            {
                if (_subCategoryViewRepo == null)
                    _subCategoryViewRepo = new GenerateAPIResponse<SubCategoryViewModel>(config);
                return _subCategoryViewRepo;
            }
        }
        #endregion

        #region [ Product View Repository ]
        /// <summary>
        /// Product View Repository
        /// </summary>
        private IGenerateAPIResponse<ProductViewModel> _productViewRepo;
        public IGenerateAPIResponse<ProductViewModel> ProductViewRepo
        {
            get
            {
                if (_productViewRepo == null)
                    _productViewRepo = new GenerateAPIResponse<ProductViewModel>(config);
                return _productViewRepo;
            }
        }
        #endregion

        #region [ Door Type View Repository ]
        /// <summary>
        /// Door Type View Repository
        /// </summary>
        private IGenerateAPIResponse<DoorTypeViewModel> _doorTypeViewRepo;
        public IGenerateAPIResponse<DoorTypeViewModel> DoorTypeViewRepo
        {
            get
            {
                if (_doorTypeViewRepo == null)
                    _doorTypeViewRepo = new GenerateAPIResponse<DoorTypeViewModel>(config);
                return _doorTypeViewRepo;
            }
        }
        #endregion

        #region [ Product Design View Repository ]
        /// <summary>
        /// Product Design View Repository
        /// </summary>
        private IGenerateAPIResponse<ProductDesignViewModel> _productDesignViewRepo;
        public IGenerateAPIResponse<ProductDesignViewModel> ProductDesignViewRepo
        {
            get
            {
                if (_productDesignViewRepo == null)
                    _productDesignViewRepo = new GenerateAPIResponse<ProductDesignViewModel>(config);
                return _productDesignViewRepo;
            }
        }
        #endregion

        #region [ Product Grade View Repository ]
        /// <summary>
        /// Product Grade View Repository
        /// </summary>
        private IGenerateAPIResponse<ProductGradeViewModel> _productGradeViewRepo;
        public IGenerateAPIResponse<ProductGradeViewModel> ProductGradeViewRepo
        {
            get
            {
                if (_productGradeViewRepo == null)
                    _productGradeViewRepo = new GenerateAPIResponse<ProductGradeViewModel>(config);
                return _productGradeViewRepo;
            }
        }
        #endregion
    }
}
