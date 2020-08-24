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
    public class ProductHeightController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public ProductHeightController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Product Heights ]
        /// <summary>
        /// Index - Load the List of Product Heights
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Product Heights Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Product Heights Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex()
        {
            IEnumerable<ProductHeightViewModel> _productHeights = await generateAPIResponse.ProductHeightViewRepo.GetAll("productHeight");
            return View("Index", _productHeights);
        }
        #endregion

        #region [ Create New Product Height ]
        /// <summary>
        /// Create New Product Height
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Product Height ]
        /// <summary>
        /// Save New Product Height
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductHeightViewModel model)
        {
            return await SaveProductHeightDetails(model, "Create");
        }
        #endregion

        #region [ Edit Product Height ]
        /// <summary>
        /// Edit Product Height
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            ProductHeightViewModel model = await generateAPIResponse.ProductHeightViewRepo.GetByID("productHeight", id);
            if (model != null)
            {
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Product Height ]
        /// <summary>
        /// Update Product Height
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductHeightViewModel model)
        {
            return await SaveProductHeightDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Product Height Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Height Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveProductHeightDetails(ProductHeightViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Height Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.ProductHeightViewRepo.Save("productHeight", model);
                    ViewBag.Message = "Product Height record has been created successfully.";
                }
                // Call Put Method to Update Existing Product Height Details
                else
                {
                    response = await generateAPIResponse.ProductHeightViewRepo.Update("productHeight/" + model.ProductHeightID, model);
                    ViewBag.Message = "Product Height record has been updated successfully.";
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

        #region [ Show Product Height Details ]
        /// <summary>
        /// Show Product Height Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductHeightViewModel model = await generateAPIResponse.ProductHeightViewRepo.GetByID("productHeight", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Product Height Record form DB. ]
        /// <summary>
        /// Delete Product Height Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await generateAPIResponse.ProductHeightViewRepo.Delete("productHeight/" + id);

                if (response)
                {
                    ViewBag.Message = "Product Height record has been deleted successfully.";
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