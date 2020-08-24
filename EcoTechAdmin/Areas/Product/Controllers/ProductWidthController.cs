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
    public class ProductWidthController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public ProductWidthController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Product Widths ]
        /// <summary>
        /// Index - Load the List of Product Widths
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Product Widths Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Product Widths Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex()
        {
            IEnumerable<ProductWidthViewModel> _productWidths = await generateAPIResponse.ProductWidthViewRepo.GetAll("productWidth");
            return View("Index", _productWidths);
        }
        #endregion

        #region [ Create New Product Width ]
        /// <summary>
        /// Create New Product Width
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Product Width ]
        /// <summary>
        /// Save New Product Width
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductWidthViewModel model)
        {
            return await SaveProductWidthDetails(model, "Create");
        }
        #endregion

        #region [ Edit Product Width ]
        /// <summary>
        /// Edit Product Width
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            ProductWidthViewModel model = await generateAPIResponse.ProductWidthViewRepo.GetByID("productWidth", id);
            if (model != null)
            {
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Product Width ]
        /// <summary>
        /// Update Product Width
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductWidthViewModel model)
        {
            return await SaveProductWidthDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Product Width Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Width Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveProductWidthDetails(ProductWidthViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Width Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.ProductWidthViewRepo.Save("productWidth", model);
                    ViewBag.Message = "Product Width record has been created successfully.";
                }
                // Call Put Method to Update Existing Product Width Details
                else
                {
                    response = await generateAPIResponse.ProductWidthViewRepo.Update("productWidth/" + model.ProductWidthID, model);
                    ViewBag.Message = "Product Width record has been updated successfully.";
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

        #region [ Show Product Width Details ]
        /// <summary>
        /// Show Product Width Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductWidthViewModel model = await generateAPIResponse.ProductWidthViewRepo.GetByID("productWidth", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Product Width Record form DB. ]
        /// <summary>
        /// Delete Product Width Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await generateAPIResponse.ProductWidthViewRepo.Delete("productWidth/" + id);

                if (response)
                {
                    ViewBag.Message = "Product Width record has been deleted successfully.";
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