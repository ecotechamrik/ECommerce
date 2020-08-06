#region [ Namespace References ]
using System.Collections.Generic;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BAL;
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

        #region [ Index - Load the List of Product Attributes & Prices ]
        /// <summary>
        /// Index - Load the List of Product Attributes & Prices
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            return await RedirectToIndex(id);
        }
        #endregion

        #region [ Load Index With Product Attributes & Prices Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Product Attributes & Prices Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex(int? id)
        {
            IEnumerable<ProductSizeAndPriceViewModel> _productattprice = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetByProductID("productattrandprices/getbyproductid", id);
            return View("Index", _productattprice);
        }
        #endregion

        #region [ Create New Product Attributes & Prices ]
        /// <summary>
        /// Create New Product Attributes & Prices
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Product Attributes & Prices ]
        /// <summary>
        /// Save New Product Attributes & Prices
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductSizeAndPriceViewModel model)
        {
            return await SaveProductAttrAndPrices(model, "Create");
        }
        #endregion

        #region [ Edit Product Attributes & Prices ]
        /// <summary>
        /// Edit Product Attributes & Prices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            ProductSizeAndPriceViewModel model = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetByID("productattrandprices", id);
            if (model != null)
            {
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Product Attributes & Prices ]
        /// <summary>
        /// Update Product Attributes & Prices
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductSizeAndPriceViewModel model)
        {
            return await SaveProductAttrAndPrices(model, "Edit");
        }
        #endregion

        #region [ Save & Update Product Attributes & Prices with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Attributes & Prices with Post & Put Methods of the Web APIs
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveProductAttrAndPrices(ProductSizeAndPriceViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Attributes & Prices
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.ProductSizeAndPriceViewRepo.Save("productattrandprices", model);
                    ViewBag.Message = "Product Attributes & Prices record has been created successfully.";
                }
                // Call Put Method to Update Existing Product Attributes & Prices
                else
                {
                    response = await generateAPIResponse.ProductSizeAndPriceViewRepo.Update("productattrandprices/" + model.ProductAttributeID, model);
                    ViewBag.Message = "Product Attributes & Prices record has been updated successfully.";
                }

                if (response)
                {
                    ViewBag.Class = "text-success";
                    return await RedirectToIndex(model.ProductID);
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

        #region [ Show Product Attributes & Prices ]
        /// <summary>
        /// Show Product Attributes & Prices
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductSizeAndPriceViewModel model = await generateAPIResponse.ProductSizeAndPriceViewRepo.GetByID("productattrandprices", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Product Attributes & Prices Record form DB. ]
        /// <summary>
        /// Delete Product Attributes & Prices Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id, int pid)
        {
            try
            {
                var response = await generateAPIResponse.ProductSizeAndPriceViewRepo.Delete("productattrandprices/" + id);

                if (response)
                {
                    ViewBag.Message = "Product Attributes & Prices record has been deleted successfully.";
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