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
    public class ProductDesignController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public ProductDesignController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Product Designs ]
        /// <summary>
        /// Index - Load the List of Product Designs
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Product Designs Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Product Designs Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex()
        {
            IEnumerable<ProductDesignViewModel> _productdesigns = await generateAPIResponse.ProductDesignViewRepo.GetAll("productdesign");
            return View("Index", _productdesigns);
        }
        #endregion

        #region [ Create New Product Design ]
        /// <summary>
        /// Create New Product Design
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Product Design ]
        /// <summary>
        /// Save New Product Design
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductDesignViewModel model)
        {
            return await SaveProductDesignDetails(model, "Create");
        }
        #endregion

        #region [ Edit Product Design ]
        /// <summary>
        /// Edit Product Design
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            ProductDesignViewModel model = await generateAPIResponse.ProductDesignViewRepo.GetByID("productdesign", id);
            if (model != null)
            {
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Product Design ]
        /// <summary>
        /// Update Product Design
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductDesignViewModel model)
        {
            return await SaveProductDesignDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Product Design Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Design Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveProductDesignDetails(ProductDesignViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Product Design Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.ProductDesignViewRepo.Save("productdesign", model);
                    ViewBag.Message = "Product Design record has been created successfully.";
                }
                // Call Put Method to Update Existing Product Design Details
                else
                {
                    response = await generateAPIResponse.ProductDesignViewRepo.Update("productdesign/" + model.ProductDesignID, model);
                    ViewBag.Message = "Product Design record has been updated successfully.";
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

        #region [ Show Product Design Details ]
        /// <summary>
        /// Show Product Design Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            ProductDesignViewModel model = await generateAPIResponse.ProductDesignViewRepo.GetByID("productdesign", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Product Design Record form DB. ]
        /// <summary>
        /// Delete Product Design Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await generateAPIResponse.ProductDesignViewRepo.Delete("productdesign/" + id);

                if (response)
                {
                    ViewBag.Message = "Product Design record has been deleted successfully.";
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