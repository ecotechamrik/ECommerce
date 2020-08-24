#region [ Namespace References ]
using System.Collections.Generic;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BAL;
using System.Linq;
using BAL.ViewModels.Common;
using BAL.Entities;
#endregion

namespace EcoTechAdmin.Areas.Product.Controllers
{
    public class ProductAttributeController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public ProductAttributeController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Product Attributes ]
        /// <summary>
        /// Index - Load the List of Product Attributes
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                TempData["ProductID"] = id;
            }
            return await RedirectToIndex(id);
        }
        #endregion

        #region [ Load Index With Product Attributes Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Product Attributes Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex(int? id)
        {
            IEnumerable<ProductAttributeViewModel> _productAttributes = new List<ProductAttributeViewModel>();

            if (id != null)
            {
                _productAttributes = await generateAPIResponse.ProductAttributeViewRepo.GetAll("ProductAttribute/GetByProductID/" + id);
                ViewBag.ProductID = id;
            }
            else
                _productAttributes = await generateAPIResponse.ProductAttributeViewRepo.GetAll("ProductAttribute");

            return View("Index", _productAttributes);
        }
        #endregion

        #region [ Create New Product Attribute ]
        /// <summary>
        /// Create New Product Attribute
        /// </summary>
        /// <returns></returns>
        [Route("[area]/[controller]/Create/{id?}/{doorTypeId?}")]
        public IActionResult Create(int? id, int? doorTypeId)
        {
            if (id != null)
            {
                ViewBag.ShowSupplierThickness = "hide";
                ProductAttributeViewModel _productAttributeViewModel = new ProductAttributeViewModel
                {
                    ProductID = id,
                    DoorTypeList = GetDoorTypes(),
                    CurrencyList = GetCurrencies()
                };
                if (doorTypeId != null)
                {                    
                    _productAttributeViewModel.ProductThicknessList = GetProductThickness(0);
                    ViewBag.ShowSupplierThickness = "show";
                }
                return View(_productAttributeViewModel);
            }
            else
                return null;
        }
        #endregion

        #region [ Get All Door Types - Bind Door Types Drop Down List ]
        /// <summary>
        /// Get All Door Types - Bind Door Types Drop Down List
        /// </summary>
        private IList<DoorTypeViewModel> GetDoorTypes()
        {
            return generateAPIResponse.DoorTypeViewRepo.GetAll("doortype").Result.ToList();
        }
        #endregion

        #region [ Get Currencies - Bind Currency Drop Down List ]
        /// <summary>
        /// Get Currencies - Bind Currency Drop Down List
        /// </summary>
        private IList<CurrencyViewModel> GetCurrencies()
        {
            return generateAPIResponse.CurrencyViewRepo.GetAll("currency").Result.ToList();
        }
        #endregion

        #region [ Get Suppliers - Bind Suppliers Drop Down List ]
        /// <summary>
        /// Get Suppliers - Bind Suppliers Drop Down List
        /// </summary>
        private IList<SupplierViewModel> GetSuppliers()
        {
            return generateAPIResponse.SupplierViewRepo.GetAll("supplier").Result.Where(s => s.IsActive == true).ToList();
        }
        #endregion

        #region [ Get Product Thickness - Bind Product Thickness Drop Down List ]
        /// <summary>
        /// GetProductThickness - Bind Product Thickness List
        /// </summary>
        private IList<ProductThicknessViewModel> GetProductThickness(int? did)
        {
            return generateAPIResponse.ProductThicknessViewRepo.GetAll("ProductThickness/GetWithAttributeThicknessID/" + did).Result.ToList();
        }
        #endregion

        #region [ Product Thickness - Load the Detail of Product Attribute Thickness ]
        /// <summary>
        /// ProductThickness - Load the Detail of Product Attribute Thickness
        /// </summary>
        /// <returns></returns>        
        [Route("[area]/[controller]/ProductThickness/{productThicknessID}/{productAttributeThicknessID}/{productThickness}")]
        public IActionResult ProductThickness(int productThicknessID, int productAttributeThicknessID, string productThickness)
        {
            ProductAttributeThicknessViewModel _productAttributeThickness;
            if (productAttributeThicknessID == 0)
                _productAttributeThickness = new ProductAttributeThicknessViewModel { ProductThicknessName = productThickness, ProductThicknessID = productThicknessID, ProductWidthList = GetProductWidths() };
            else
            {
                _productAttributeThickness = generateAPIResponse.ProductAttributeThicknessViewRepo.GetByID("ProductAttributeThickness/", productAttributeThicknessID).Result;
                _productAttributeThickness.ProductThicknessName = productThickness;
                _productAttributeThickness.ProductWidthList = GetProductWidths();
            }

            return PartialView("_ProductThickness", _productAttributeThickness);
        }
        #endregion

        #region [ Product Heights - Load the Product Height Details Based on ProductAttributeThicknessID & ProductWidthID ]
        /// <summary>
        /// Product Heights - Load the Product Height Details Based on ProductAttributeThicknessID & ProductWidthID
        /// </summary>
        /// <returns></returns>        
        [Route("[area]/[controller]/ProductHeights/{ProductAttributeThicknessID}/{ProductWidthID}/{ProductThickness}/{ProductWidth}")]
        public async Task<IActionResult> ProductThicknessHeights(int ProductAttributeThicknessID, int ProductWidthID, string ProductThickness, string ProductWidth)
        {
            IEnumerable<ProductSizeAndPriceViewModel> _productAttributeThicknesses = new List<ProductSizeAndPriceViewModel>();

            _productAttributeThicknesses = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetAll("ProductSizeAndPrice/GetByProductWidthID/" + ProductAttributeThicknessID + "/" + ProductWidthID);

            _productAttributeThicknesses.First().SupplierList = GetSuppliers();
            _productAttributeThicknesses.First().ThicknessWidth = ProductThickness + "_" + ProductWidth;

            return PartialView("_ProductHeights", _productAttributeThicknesses);
        }
        #endregion

        #region [ Get Product Width List ]
        /// <summary>
        /// Get Product Width List
        /// </summary>
        /// <returns></returns>
        private IList<ProductWidthViewModel> GetProductWidths()
        {
            return generateAPIResponse.ProductWidthViewRepo.GetAll("productWidth").Result.ToList();
        }
        #endregion

        #region [ Save New Product Attribute ]
        /// <summary>
        /// Save New Product Attribute
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductAttributeViewModel model)
        {
            return await SaveProductAttributeDetails(model, "Create");
        }
        #endregion

        #region [ Edit Product Attribute ]
        /// <summary>
        /// Edit Product Attribute
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            ProductAttributeViewModel model = await generateAPIResponse.ProductAttributeViewRepo.GetByID("ProductAttribute", id);
            if (model != null)
            {
                GetDoorTypes();
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Product Attribute ]
        /// <summary>
        /// Update Product Attribute
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductAttributeViewModel model)
        {
            return await SaveProductAttributeDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Product Attribute Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Attribute Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveProductAttributeDetails(ProductAttributeViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Attribute Details
                if (action.ToLower() == "create")
                {
                    model = await generateAPIResponse.ProductAttributeViewRepo.SaveModel("ProductAttribute", model);
                    if (model.ProductAttributeID != 0)
                    {
                        response = true;
                        ViewBag.Message = "Product Attribute record has been created successfully.";
                    }
                }
                // Call Put Method to Update Existing Product Attribute Details
                else
                {
                    response = await generateAPIResponse.ProductAttributeViewRepo.Update("ProductAttribute/" + model.ProductAttributeID, model);
                    ViewBag.Message = "Product Attribute record has been updated successfully.";
                }

                if (response)
                {
                    ViewBag.Class = "text-success";
                    return RedirectToAction("Details", new { id = model.ProductAttributeID });
                }
                else
                {
                    ViewBag.Message = null;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            return View("Create", model);
        }
        #endregion

        #region [ Show Product Attribute Details ]
        /// <summary>
        /// Show Product Attribute Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductAttributeViewModel model = await generateAPIResponse.ProductAttributeViewRepo.GetProductAttrWithDoorName("ProductAttribute/GetProductAttrWithDoorName", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Product Attribute Record form DB ]
        /// <summary>
        /// Delete Product Attribute Record form DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[area]/[controller]/{Delete}/{id}/{ProductID}")]
        public async Task<IActionResult> Delete(int id, int ProductID)
        {
            try
            {
                var response = await generateAPIResponse.ProductAttributeViewRepo.Delete("ProductAttribute/" + id);

                if (response)
                {
                    ViewBag.Message = "Product Attribute record has been deleted successfully.";
                    return await RedirectToIndex(ProductID);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
        #endregion
    }
}