#region [ Namespace References ]
using System.Collections.Generic;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BAL;
using BAL.Entities;
using System.Linq;
using BAL.ViewModels.Common;
#endregion

namespace EcoTechAdmin.Areas.Product.Controllers
{
    public class ProductSizeAndPriceController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public ProductSizeAndPriceController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Product Size & Prices ]
        /// <summary>
        /// Index - Load the List of Product Size & Prices
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            return await RedirectToIndex(id);
        }
        #endregion

        #region [ Load Index With Product Size & Prices Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Product Size & Prices Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex(int? id)
        {
            IEnumerable<ProductSizeAndPriceViewModel> _productSizeAndPrice = new List<ProductSizeAndPriceViewModel>();

            if (id != null)
            {

                _productSizeAndPrice = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetAll("ProductSizeAndPrice/GetByProductAttributeID/" + id);
                
                ViewBag.AddNew = true; // Hide Add New Icon when all sizes already added into the list.
                if (_productSizeAndPrice.Count() > 0 && _productSizeAndPrice.FirstOrDefault().MoreProductSizes == 0)
                    ViewBag.AddNew = false;
                ViewBag.ProductAttributeID = id;
            }
            else
                _productSizeAndPrice = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetAll("ProductSizeAndPrice");
            return View("Index", _productSizeAndPrice);
        }
        #endregion

        #region [ Bind Product Size, Currency, Supplier Drop Down Lists ]
        private void BindDropDownLists(int? _pAttrID, int? _pPriceSizePriceID)
        {
            GetProductSizes(_pAttrID, _pPriceSizePriceID); // Bind Product Sizes Drop Down list
            GetCurrencies();    // Bind Currency Drop Down list
            GetSuppliers();    // Bind Suppliers Drop Down list
        }

        #region [ Get Product Sizes - Bind Product Sizes Drop Down List ]
        /// <summary>
        /// Get Product Sizes - Bind Product Sizes Drop Down List
        /// </summary>
        private void GetProductSizes(int? _pAttrID, int? _pPriceSizePriceID)
        {
            string _method = "productsize/GetProductSizeNotAdded/" + _pAttrID + "/" + _pPriceSizePriceID;
            IEnumerable<ProductHeightViewModel> _productSizes = generateAPIResponse.ProductHeightViewRepo.GetAll(_method).Result;

            if (_productSizes != null)
            {
                ViewBag.ProductSizes = _productSizes;
            }
        }
        #endregion

        #region [ Get Currencies - Bind Currency Drop Down List ]
        /// <summary>
        /// Get Currencies - Bind Currency Drop Down List
        /// </summary>
        private void GetCurrencies()
        {
            IEnumerable<CurrencyViewModel> _currencies = generateAPIResponse.CurrencyViewRepo.GetAll("currency").Result;

            if (_currencies != null)
            {
                ViewBag.Currencies = _currencies;
            }
        }
        #endregion

        #region [ Get Suppliers - Bind Suppliers Drop Down List ]
        /// <summary>
        /// Get Suppliers - Bind Suppliers Drop Down List
        /// </summary>
        private void GetSuppliers()
        {
            IEnumerable<SupplierViewModel> _suppliers = generateAPIResponse.SupplierViewRepo.GetAll("supplier").Result;

            if (_suppliers != null)
            {
                ViewBag.Suppliers = _suppliers;
            }
        }
        #endregion

        #endregion

        #region [ Create New Product Size & Prices ]
        /// <summary>
        /// Create New Product Size & Prices
        /// </summary>
        /// <returns></returns>
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.ProductAttributeID = id;
                BindDropDownLists(id, 0);   // Bind All Drop Down Lists
                return View(new ProductSizeAndPriceViewModel { PriceDate = DateTime.Now, InvDate = DateTime.Now, SupplierList = ViewBag.Suppliers });
            }
            else
                return null;
        }
        #endregion

        #region [ Save New Product Size & Prices ]
        /// <summary>
        /// Save New Product Size & Prices
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductSizeAndPriceViewModel model)
        {
            model.PriceDate = DateTime.Now;
            model.InvDate = DateTime.Now;
            model.CreatedDateTime = DateTime.Now;
            model.UpdatedDateTime = DateTime.Now;
            return await SaveProductAttrAndPrices(model, "Create");
        }
        #endregion

        #region [ Edit Product Size & Prices ]
        /// <summary>
        /// Edit Product Size & Prices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            ProductSizeAndPriceViewModel model = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetByID("ProductSizeAndPrice", id);
            if (model != null)
            {
                ViewBag.ProductAttributeID = model.ProductAttributeID;
                BindDropDownLists(model.ProductAttributeID, model.ProductSizeAndPriceID);   // Bind All Drop Down Lists
                model.SupplierList = ViewBag.Suppliers;
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Product Size & Prices ]
        /// <summary>
        /// Update Product Size & Prices
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductSizeAndPriceViewModel model)
        {
            model.UpdatedDateTime = DateTime.Now;
            return await SaveProductAttrAndPrices(model, "Edit");
        }
        #endregion

        #region [ Save & Update Product Size & Prices with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Size & Prices with Post & Put Methods of the Web APIs
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveProductAttrAndPrices(ProductSizeAndPriceViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Size & Prices
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.ProductSizeAndPriceViewRepo.Save("ProductSizeAndPrice", model);
                    ViewBag.Message = "Product Size & Prices record has been created successfully.";
                }
                // Call Put Method to Update Existing Product Size & Prices
                else
                {
                    response = await generateAPIResponse.ProductSizeAndPriceViewRepo.Update("ProductSizeAndPrice/" + model.ProductSizeAndPriceID, model);
                    ViewBag.Message = "Product Size & Prices record has been updated successfully.";
                }

                if (response)
                {
                    ViewBag.Class = "text-success";
                    return await RedirectToIndex(model.ProductAttributeID);
                }
                else
                {
                    ViewBag.Message = null;
                    return View("Create", model);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            return View("Create", model);
        }
        #endregion

        #region [ Show Product Size & Prices ]
        /// <summary>
        /// Show Product Size & Prices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductSizeAndPriceViewModel model = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetByID("ProductSizeAndPrice", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Product Size & Prices Record form DB ]
        /// <summary>
        /// Delete Product Size & Prices Record form DB
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id, int pid)
        {
            try
            {
                var response = await generateAPIResponse.ProductSizeAndPriceViewRepo.Delete("ProductSizeAndPrice/" + id);

                if (response)
                {
                    ViewBag.Message = "Product Size & Prices record has been deleted successfully.";
                    return await RedirectToIndex(pid);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            return RedirectToAction("Index", pid);
        }
        #endregion
    }
}