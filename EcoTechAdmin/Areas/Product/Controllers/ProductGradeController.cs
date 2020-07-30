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
    public class ProductGradeController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IGenerateAPIResponse<ProductGradeViewModel> generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls]
        public ProductGradeController(IGenerateAPIResponse<ProductGradeViewModel> _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Product Grades ]
        /// <summary>
        /// Index - Load the List of Product Grades
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Product Grades Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Product Grades Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex()
        {
            IEnumerable<ProductGradeViewModel> _productgrades = await generateAPIResponse.GetAll("productgrade");
            return View("Index", _productgrades);
        }
        #endregion

        #region [ Create New Product Grade ]
        /// <summary>
        /// Create New Product Grade
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Product Grade ]
        /// <summary>
        /// Save New Product Grade
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductGradeViewModel model)
        {
            return await SaveProductGradeDetails(model, "Create");
        }
        #endregion

        #region [ Edit Product Grade ]
        /// <summary>
        /// Edit Product Grade
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            ProductGradeViewModel model = await generateAPIResponse.GetByID("productgrade", id);
            if (model != null)
            {
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Product Grade ]
        /// <summary>
        /// Update Product Grade
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductGradeViewModel model)
        {
            return await SaveProductGradeDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Product Grade Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Grade Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveProductGradeDetails(ProductGradeViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Grade Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.Save("productgrade", model);
                    ViewBag.Message = "Product Grade record has been created successfully.";
                }
                // Call Put Method to Update Existing Product Grade Details
                else
                {
                    response = await generateAPIResponse.Update("productgrade/" + model.ProductGradeID, model);
                    ViewBag.Message = "Product Grade record has been updated successfully.";
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

        #region [ Show Product Grade Details ]
        /// <summary>
        /// Show Product Grade Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductGradeViewModel model = await generateAPIResponse.GetByID("productgrade", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Product Grade Record form DB. ]
        /// <summary>
        /// Delete Product Grade Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await generateAPIResponse.Delete("productgrade/" + id);

                if (response)
                {
                    ViewBag.Message = "Product Grade record has been deleted successfully.";
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