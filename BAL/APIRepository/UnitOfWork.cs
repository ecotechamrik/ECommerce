#region [ Namespace ]
using BAL.Entities;
using BAL.ViewModels;
using BAL.ViewModels.Common;
using BAL.ViewModels.Product;
using Microsoft.Extensions.Configuration;
using System;
#endregion

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

        #region [ Currency View Repository ]
        /// <summary>
        /// Currency View Repository
        /// </summary>
        private IGenerateAPIResponse<CurrencyViewModel> _currencyViewRepo;
        public IGenerateAPIResponse<CurrencyViewModel> CurrencyViewRepo
        {
            get
            {
                if (_currencyViewRepo == null)
                    _currencyViewRepo = new GenerateAPIResponse<CurrencyViewModel>(config);
                return _currencyViewRepo;
            }
        }
        #endregion

        #region [ Section View Repository ]
        /// <summary>
        /// Section View Repository
        /// </summary>
        private IGenerateAPIResponse<SectionViewModel> _sectionViewRepo;
        public IGenerateAPIResponse<SectionViewModel> SectionViewRepo
        {
            get
            {
                if (_sectionViewRepo == null)
                    _sectionViewRepo = new GenerateAPIResponse<SectionViewModel>(config);
                return _sectionViewRepo;
            }
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

        #region [ Sub Cat Gallery View Repository ]
        /// <summary>
        /// Sub Cat Gallery View Repository
        /// </summary>
        private ISubCatGalleryAPIResponse _subCatGalleryViewRepo;
        public ISubCatGalleryAPIResponse SubCatGalleryViewRepo
        {
            get
            {
                if (_subCatGalleryViewRepo == null)
                    _subCatGalleryViewRepo = new SubCatGalleryAPIResponse(config);
                return _subCatGalleryViewRepo;
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

        #region [ Product Height View Repository ]
        /// <summary>
        /// Product Height View Repository
        /// </summary>
        private IGenerateAPIResponse<ProductHeightViewModel> _productHeightViewRepo;
        public IGenerateAPIResponse<ProductHeightViewModel> ProductHeightViewRepo
        {
            get
            {
                if (_productHeightViewRepo == null)
                    _productHeightViewRepo = new GenerateAPIResponse<ProductHeightViewModel>(config);
                return _productHeightViewRepo;
            }
        }
        #endregion        

        #region [ Product Width View Repository ]
        /// <summary>
        /// Product Width View Repository
        /// </summary>
        private IGenerateAPIResponse<ProductWidthViewModel> _productWidthViewRepo;
        public IGenerateAPIResponse<ProductWidthViewModel> ProductWidthViewRepo
        {
            get
            {
                if (_productWidthViewRepo == null)
                    _productWidthViewRepo = new GenerateAPIResponse<ProductWidthViewModel>(config);
                return _productWidthViewRepo;
            }
        }
        #endregion        

        #region [ Product Thickness View Repository ]
        /// <summary>
        /// Product Thickness View Repository
        /// </summary>
        private IGenerateAPIResponse<ProductThicknessViewModel> _productThicknessViewRepo;
        public IGenerateAPIResponse<ProductThicknessViewModel> ProductThicknessViewRepo
        {
            get
            {
                if (_productThicknessViewRepo == null)
                    _productThicknessViewRepo = new GenerateAPIResponse<ProductThicknessViewModel>(config);
                return _productThicknessViewRepo;
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

        #region [ Product Attribute View Repository ]
        /// <summary>
        /// Product Attribute View Repository
        /// </summary>
        private IProductAttributeAPIResponse _productAttributeViewRepo;
        public IProductAttributeAPIResponse ProductAttributeViewRepo
        {
            get
            {
                if (_productAttributeViewRepo == null)
                    _productAttributeViewRepo = new ProductAttributeAPIResponse(config);
                return _productAttributeViewRepo;
            }
        }
        #endregion

        #region [ Product Attribute Thickness View Repository ]
        /// <summary>
        /// Product Attribute Thickness View Repository
        /// </summary>
        private IProductAttributeThicknessAPIResponse _productAttributeThicknessViewRepo;
        public IProductAttributeThicknessAPIResponse ProductAttributeThicknessViewRepo
        {
            get
            {
                if (_productAttributeThicknessViewRepo == null)
                    _productAttributeThicknessViewRepo = new ProductAttributeThicknessAPIResponse(config);
                return _productAttributeThicknessViewRepo;
            }
        }
        #endregion        

        #region [ Supplier View Repository ]
        /// <summary>
        /// Supplier View Repository
        /// </summary>
        private IGenerateAPIResponse<SupplierViewModel> _supplierViewRepo;
        public IGenerateAPIResponse<SupplierViewModel> SupplierViewRepo
        {
            get
            {
                if (_supplierViewRepo == null)
                    _supplierViewRepo = new GenerateAPIResponse<SupplierViewModel>(config);
                return _supplierViewRepo;
            }
        }
        #endregion

        #region [ Product Sizes And Prices View Repository ]
        /// <summary>
        /// Product Sizes And Prices View Repository
        /// </summary>
        private IProductSizeAndPriceAPIResponse _productSizeAndPriceViewRepo;
        public IProductSizeAndPriceAPIResponse ProductSizeAndPriceViewRepo
        {
            get
            {
                if (_productSizeAndPriceViewRepo == null)
                    _productSizeAndPriceViewRepo = new ProductSizeAndPriceAPIResponse(config);
                return _productSizeAndPriceViewRepo;
            }
        }
        #endregion

        #region [ Product Supplier View Repository ]
        /// <summary>
        /// Product Supplier View Repository
        /// </summary>
        private IProductSupplierAPIResponse _productSupplierViewRepo;
        public IProductSupplierAPIResponse ProductSupplierViewRepo
        {
            get
            {
                if (_productSupplierViewRepo == null)
                    _productSupplierViewRepo = new ProductSupplierAPIResponse(config);
                return _productSupplierViewRepo;
            }
        }
        #endregion

        #region [ Product Images View Repository ]
        /// <summary>
        /// Product Images View Repository
        /// </summary>
        private IProductImagesAPIResponse _productImagesViewRepo;
        public IProductImagesAPIResponse ProductImagesViewRepo
        {
            get
            {
                if (_productImagesViewRepo == null)
                    _productImagesViewRepo = new ProductImagesAPIResponse(config);
                return _productImagesViewRepo;
            }
        }
        #endregion

        #region [ Website Info View Repository ]
        /// <summary>
        /// Website Info View Repository
        /// </summary>
        private IGenerateAPIResponse<WebsiteInfoViewModel> _websiteInfoViewRepo;
        public IGenerateAPIResponse<WebsiteInfoViewModel> WebsiteInfoViewRepo
        {
            get
            {
                if (_websiteInfoViewRepo == null)
                    _websiteInfoViewRepo = new GenerateAPIResponse<WebsiteInfoViewModel>(config);
                return _websiteInfoViewRepo;
            }
        }
        #endregion

        #region [ Common Repository ]
        /// <summary>
        /// Common Repository
        /// </summary>
        private IGenerateAPIResponse<String> _commonRepo;
        public IGenerateAPIResponse<String> CommonRepo
        {
            get
            {
                if (_commonRepo == null)
                    _commonRepo = new GenerateAPIResponse<String>(config);
                return _commonRepo;
            }
        }
        #endregion
    }
}
