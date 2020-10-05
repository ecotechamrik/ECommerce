#region [ Namespace References ]
using System.Collections.Generic;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BAL;
using System.Linq;
using BAL.ViewModels.Common;
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
                ProductViewModel model = await generateAPIResponse.ProductViewRepo.GetByID("product", id);
                TempData["ProductName"] = model.ProductName;
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
                _productAttributes = await generateAPIResponse.ProductAttributeViewRepo.GetAll("ProductAttribute/GetAttributesByProductID/" + id);
                ViewBag.ProductID = id;
            }
            else
                _productAttributes = await generateAPIResponse.ProductAttributeViewRepo.GetAll("ProductAttribute");

            return View("Index", _productAttributes);
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
        private IList<ProductThicknessViewModel> GetProductThickness(int? ProductAttributeID)
        {
            return generateAPIResponse.ProductThicknessViewRepo.GetAll("ProductThickness/GetWithAttributeThicknessID/" + ProductAttributeID).Result.ToList();
        }
        #endregion

        #region [ Get Product Thickness - Load the Detail of Product Attribute Thickness ]
        /// <summary>
        /// ProductThickness - Load the Detail of Product Attribute Thickness
        /// </summary>
        /// <returns></returns>        
        [Route("[area]/[controller]/ProductThickness/{productThicknessID}/{productAttributeThicknessID}/{productThickness}")]
        public async Task<IActionResult> ProductThickness(int productThicknessID, int productAttributeThicknessID, string productThickness)
        {
            ProductAttributeThicknessViewModel _productAttributeThickness;
            if (productAttributeThicknessID == 0)
                _productAttributeThickness = new ProductAttributeThicknessViewModel { ProductThicknessName = productThickness, ProductThicknessID = productThicknessID, ProductWidthList = GetProductWidths() };
            else
            {
                _productAttributeThickness = await generateAPIResponse.ProductAttributeThicknessViewRepo.GetByID("ProductAttributeThickness", productAttributeThicknessID);
                _productAttributeThickness.ProductThicknessName = productThickness;
                _productAttributeThickness.ProductWidthList = GetProductWidths();
            }

            return PartialView("_ProductThickness", _productAttributeThickness);
        }
        #endregion

        #region [ Get Product Widths - Load the Product Height Details Based on ProductAttributeThicknessID & ProductWidthID ]
        /// <summary>
        /// Product Widths - Load the Product Height Details Based on ProductAttributeThicknessID & ProductWidthID
        /// </summary>
        /// <returns></returns>        
        [Route("[area]/[controller]/ProductThicknessHeights/{ProductAttributeThicknessID}/{ProductThicknessID}/{ProductWidthID}/{ProductThickness}/{ProductWidth}")]
        public async Task<IActionResult> ProductThicknessHeights(int ProductAttributeThicknessID, int ProductThicknessID, int ProductWidthID, string ProductThickness, string ProductWidth)
        {
            IEnumerable<ProductSizeAndPriceViewModel> _productAttributeThicknesses = new List<ProductSizeAndPriceViewModel>();

            _productAttributeThicknesses = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetAll("ProductSizeAndPrice/GetByProductWidthID/" + ProductAttributeThicknessID + "/" + ProductWidthID);

            foreach (ProductSizeAndPriceViewModel item in _productAttributeThicknesses)
            {
                item.ProductSuppliers = GetProductSupplier(item.ProductSizeAndPriceID);
            }

            _productAttributeThicknesses.First().ThicknessIDWidthID = ProductThicknessID + "_" + ProductWidthID;
            _productAttributeThicknesses.First().ThicknessName = ProductThickness;
            _productAttributeThicknesses.First().WidthName = ProductWidth;

            return PartialView("_ProductHeights", _productAttributeThicknesses);
        }
        #endregion

        #region [ Get Product Suppliers - Load the Product Supplier Details Based on ProductSizeAndPriceID ]
        /// <summary>
        /// Product Suppliers - Load the Product Supplier Details Based on ProductSizeAndPriceID
        /// </summary>
        /// <returns></returns>        
        public IList<ProductSupplierViewModel> GetProductSupplier(int ProductSizeAndPriceID)
        {
            IEnumerable<ProductSupplierViewModel> _productSuppliers = new List<ProductSupplierViewModel>();

            _productSuppliers = generateAPIResponse.ProductSupplierViewRepo.GetAll("ProductSupplier/GetByProductSizeAndPriceID/" + ProductSizeAndPriceID).Result;
            return _productSuppliers.ToList();
        }
        #endregion

        #region [ Get Product Attribute Details - Load the Product Attribute Details Based on ProductAttributeID for the Main Index Page ]
        /// <summary>
        /// Get Product Attribute Details - Load the Product Attribute Details Based on ProductAttributeID for the Main Index Page
        /// </summary>
        /// <returns></returns>        
        [Route("[area]/[controller]/ProductAttributeDetails/{ProductAttributeID}")]
        public async Task<IActionResult> ProductAttributeDetails(int ProductAttributeID)
        {
            IEnumerable<ProductSizeAndPriceViewModel> _productAttributeThicknesses = new List<ProductSizeAndPriceViewModel>();

            _productAttributeThicknesses = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetAll("ProductSizeAndPrice/ProductAttributeDetails/" + ProductAttributeID);

            return PartialView("_ProductAttributeDetails", _productAttributeThicknesses);
        }
        #endregion

        #region [ Get Product Widths List ]
        /// <summary>
        /// Get Product Widths List
        /// </summary>
        /// <returns></returns>
        private IList<ProductWidthViewModel> GetProductWidths()
        {
            return generateAPIResponse.ProductWidthViewRepo.GetAll("productWidth").Result.ToList();
        }
        #endregion

        #region [ Create New Product Attribute ]
        /// <summary>
        /// Create New Product Attribute
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[area]/[controller]/Create/{id?}/{ProductAttributeID?}")]
        public IActionResult Create(int? id, int? ProductAttributeID)
        {
            if (id != null)
            {
                ProductAttributeViewModel _productAttributeViewModel = new ProductAttributeViewModel
                {
                    ProductID = id,
                    ProductAttributeID = 0,
                    CurrencyList = GetCurrencies(),
                    ProductThicknessList = GetProductThickness(0)
                };
                return View(_productAttributeViewModel);
            }
            else
                return null;
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
            ProductAttributeViewModel model = await generateAPIResponse.ProductAttributeViewRepo.GetByID("ProductAttribute/GetAttributesByID", id);
            if (model != null)
            {
                model.CurrencyList = GetCurrencies();
                model.ProductThicknessList = GetProductThickness(id);
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
                    }
                }
                // Call Put Method to Update Existing Product Attribute Details
                else
                {
                    response = await generateAPIResponse.ProductAttributeViewRepo.Update("ProductAttribute/" + model.ProductAttributeID, model);
                }

                if (response)
                {
                    TempData["Message"] = "Product Attribute record has been updated successfully.";
                    TempData["Class"] = "text-success";
                    return Ok(model.ProductAttributeID);
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
            return StatusCode(500);
        }
        #endregion

        #region [ Save & Update Product Attribute Thickness Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Attribute Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveProductAttributeThickness(ProductAttributeThicknessViewModel model)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Attribute Details
                if (model.FormAction.ToLower() == "create")
                {
                    model = await generateAPIResponse.ProductAttributeThicknessViewRepo.SaveModel("ProductAttributeThickness", model);
                    if (model.ProductAttributeThicknessID != 0)
                    {
                        response = true;
                    }
                }
                // Call Put Method to Update Existing Product Attribute Details
                else
                {
                    response = await generateAPIResponse.ProductAttributeThicknessViewRepo.Update("ProductAttributeThickness/" + model.ProductAttributeThicknessID, model);
                }

                if (response)
                {
                    return Ok(model.ProductAttributeThicknessID);
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
            return StatusCode(500);
        }
        #endregion

        #region [ Save & Update Product Size and Price Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Size and Price with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveProductSizeAndPrice(ProductSizeAndPriceViewModel model)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Size and Price
                if (model.FormAction.ToLower() == "create")
                {
                    model = await generateAPIResponse.ProductSizeAndPriceViewRepo.SaveModel("ProductSizeAndPrice", model);
                    if (model.ProductSizeAndPriceID != 0)
                    {
                        response = true;
                    }
                }
                // Call Put Method to Update Existing Product Attribute Details
                else
                {
                    response = await generateAPIResponse.ProductSizeAndPriceViewRepo.Update("ProductSizeAndPrice/" + model.ProductSizeAndPriceID, model);
                }

                if (response)
                {
                    return Ok(model.ProductSizeAndPriceID);
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
            return StatusCode(500);
        }
        #endregion

        #region [ Save & Update Product Supplier Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Supplier Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveProductSupplier(ProductSupplierViewModel model)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Size and Price
                if (model.FormAction.ToLower() == "create")
                {
                    model = await generateAPIResponse.ProductSupplierViewRepo.SaveModel("ProductSupplier", model);
                    if (model.ProductSupplierID != 0)
                    {
                        response = true;
                    }
                }
                // Call Put Method to Update Existing Product Attribute Details
                else
                {
                    response = await generateAPIResponse.ProductSupplierViewRepo.Update("ProductSupplier/" + model.ProductSupplierID, model);
                }

                if (response)
                {
                    return Ok(model.ProductSupplierID);
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
            return StatusCode(500);
        }
        #endregion

        #region [ Update Image Order ]
        /// <summary>
        /// Update Image Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[area]/[controller]/{action=UpdatePriceVoid}/{id?}/{pricevoid?}")]
        public async Task<IActionResult> UpdatePriceVoid(int id, double pricevoid)
        {
            try
            {
                if (await generateAPIResponse.ProductSizeAndPriceViewRepo.UpdatePriceVoid("ProductSizeAndPrice/UpdatePriceVoid", id, pricevoid))
                    return StatusCode(200, "Updated Successfull");
                else
                    return StatusCode(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
                TempData["Message"] = "Something went wrong: " + ex.Message;
                return StatusCode(500);
            }
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
                    TempData["Message"] = "Product Attribute record has been deleted successfully.";
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