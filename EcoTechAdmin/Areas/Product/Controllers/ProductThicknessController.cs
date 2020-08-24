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
    public class ProductThicknessController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public ProductThicknessController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Product Thicknesss ]
        /// <summary>
        /// Index - Load the List of Product Thicknesss
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Product Thicknesss Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Product Thicknesss Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex()
        {
            IEnumerable<ProductThicknessViewModel> _productThicknesss = await generateAPIResponse.ProductThicknessViewRepo.GetAll("productThickness");
            return View("Index", _productThicknesss);
        }
        #endregion

        #region [ Create New Product Thickness ]
        /// <summary>
        /// Create New Product Thickness
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Product Thickness ]
        /// <summary>
        /// Save New Product Thickness
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductThicknessViewModel model)
        {
            return await SaveProductThicknessDetails(model, "Create");
        }
        #endregion

        #region [ Edit Product Thickness ]
        /// <summary>
        /// Edit Product Thickness
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            ProductThicknessViewModel model = await generateAPIResponse.ProductThicknessViewRepo.GetByID("productThickness", id);
            if (model != null)
            {
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Product Thickness ]
        /// <summary>
        /// Update Product Thickness
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductThicknessViewModel model)
        {
            return await SaveProductThicknessDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Product Thickness Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Thickness Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveProductThicknessDetails(ProductThicknessViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Thickness Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.ProductThicknessViewRepo.Save("productThickness", model);
                    ViewBag.Message = "Product Thickness record has been created successfully.";
                }
                // Call Put Method to Update Existing Product Thickness Details
                else
                {
                    response = await generateAPIResponse.ProductThicknessViewRepo.Update("productThickness/" + model.ProductThicknessID, model);
                    ViewBag.Message = "Product Thickness record has been updated successfully.";
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

        #region [ Show Product Thickness Details ]
        /// <summary>
        /// Show Product Thickness Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductThicknessViewModel model = await generateAPIResponse.ProductThicknessViewRepo.GetByID("productThickness", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Product Thickness Record form DB. ]
        /// <summary>
        /// Delete Product Thickness Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await generateAPIResponse.ProductThicknessViewRepo.Delete("productThickness/" + id);

                if (response)
                {
                    ViewBag.Message = "Product Thickness record has been deleted successfully.";
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