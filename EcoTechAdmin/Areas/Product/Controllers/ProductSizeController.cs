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
    public class ProductSizeController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public ProductSizeController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Product Sizes ]
        /// <summary>
        /// Index - Load the List of Product Sizes
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Product Sizes Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Product Sizes Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex()
        {
            IEnumerable<ProductSizeViewModel> _productSizes = await generateAPIResponse.ProductSizeViewRepo.GetAll("productsize");
            return View("Index", _productSizes);
        }
        #endregion

        #region [ Create New Product Size ]
        /// <summary>
        /// Create New Product Size
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Product Size ]
        /// <summary>
        /// Save New Product Size
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductSizeViewModel model)
        {
            return await SaveProductSizeDetails(model, "Create");
        }
        #endregion

        #region [ Edit Product Size ]
        /// <summary>
        /// Edit Product Size
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            ProductSizeViewModel model = await generateAPIResponse.ProductSizeViewRepo.GetByID("productsize", id);
            if (model != null)
            {
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Product Size ]
        /// <summary>
        /// Update Product Size
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductSizeViewModel model)
        {
            return await SaveProductSizeDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Product Size Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Size Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveProductSizeDetails(ProductSizeViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Size Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.ProductSizeViewRepo.Save("productsize", model);
                    ViewBag.Message = "Product Size record has been created successfully.";
                }
                // Call Put Method to Update Existing Product Size Details
                else
                {
                    response = await generateAPIResponse.ProductSizeViewRepo.Update("productsize/" + model.ProductSizeID, model);
                    ViewBag.Message = "Product Size record has been updated successfully.";
                }

                if (response)
                {
                    ViewBag.Class = "text-success";
                    return await RedirectToIndex();
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

        #region [ Show Product Size Details ]
        /// <summary>
        /// Show Product Size Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductSizeViewModel model = await generateAPIResponse.ProductSizeViewRepo.GetByID("productsize", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Product Size Record form DB. ]
        /// <summary>
        /// Delete Product Size Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await generateAPIResponse.ProductSizeViewRepo.Delete("productsize/" + id);

                if (response)
                {
                    ViewBag.Message = "Product Size record has been deleted successfully.";
                    return await RedirectToIndex();
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