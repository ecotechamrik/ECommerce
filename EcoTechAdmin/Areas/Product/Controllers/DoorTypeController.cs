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
    public class DoorTypeController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IGenerateAPIResponse<DoorTypeViewModel> generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls]
        public DoorTypeController(IGenerateAPIResponse<DoorTypeViewModel> _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Door Types ]
        /// <summary>
        /// Index - Load the List of Door Types
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Door Types Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Door Types Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex()
        {
            IEnumerable<DoorTypeViewModel> _doorTypes = await generateAPIResponse.GetAll("doortype");
            return View("Index", _doorTypes);
        }        
        #endregion

        #region [ Create New Door Type ]
        /// <summary>
        /// Create New Door Type
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Door Type ]
        /// <summary>
        /// Save New Door Type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(DoorTypeViewModel model)
        {
            return await SaveDoorTypeDetails(model, "Create");
        }
        #endregion

        #region [ Edit Door Type ]
        /// <summary>
        /// Edit Door Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            DoorTypeViewModel model = await generateAPIResponse.GetByID("doortype", id);
            if(model!=null)
            {
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Door Type ]
        /// <summary>
        /// Update Door Type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(DoorTypeViewModel model)
        {
            return await SaveDoorTypeDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Door Type Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Door Type Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveDoorTypeDetails(DoorTypeViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Door Type Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.Save("doortype", model);
                    ViewBag.Message = "Door Type record has been created successfully.";
                }
                // Call Put Method to Update Existing Door Type Details
                else
                {
                    response = await generateAPIResponse.Update("doortype/" + model.DoorTypeID, model);
                    ViewBag.Message = "Door Type record has been updated successfully.";
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

        #region [ Show Door Type Details ]
        /// <summary>
        /// Show Door Type Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            DoorTypeViewModel model = await generateAPIResponse.GetByID("doortype", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Door Type Record form DB. ]
        /// <summary>
        /// Delete Door Type Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await generateAPIResponse.Delete("doortype/" + id);

                if (response)
                {
                    ViewBag.Message = "Door Type record has been deleted successfully.";
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