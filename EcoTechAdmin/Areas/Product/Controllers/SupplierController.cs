#region [ Namespace References ]
using System.Collections.Generic;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using BAL;
using System.Linq;
#endregion

namespace EcoTechAdmin.Areas.Product.Controllers
{
    public class SupplierController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public SupplierController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Suppliers ]
        /// <summary>
        /// Index - Load the List of Suppliers
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Suppliers Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Suppliers Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private IActionResult RedirectToIndex()
        {
            IEnumerable<SupplierViewModel> _suppliers = generateAPIResponse.SupplierViewRepo.GetAll("supplier").Result.OrderByDescending(s => s.Active);
            return View("Index", _suppliers);
        }
        #endregion

        #region [ Create New Supplier ]
        /// <summary>
        /// Create New Supplier
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View(new SupplierViewModel());
        }
        #endregion

        #region [ Save New Supplier ]
        /// <summary>
        /// Save New Supplier
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(SupplierViewModel model)
        {
            model.CreatedDateTime = DateTime.Now;
            model.UpdatedDateTime = DateTime.Now;
            return await SaveSupplierDetails(model, "Create");
        }
        #endregion

        #region [ Edit Supplier ]
        /// <summary>
        /// Edit Supplier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            SupplierViewModel model = await generateAPIResponse.SupplierViewRepo.GetByID("supplier", id);
            if(model!=null)
            {
                return View("Create", model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Supplier ]
        /// <summary>
        /// Update Supplier
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(SupplierViewModel model)
        {
            model.UpdatedDateTime = DateTime.Now;
            return await SaveSupplierDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Supplier Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Supplier Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveSupplierDetails(SupplierViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Supplier Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.SupplierViewRepo.Save("supplier", model);
                    ViewBag.Message = "Supplier record has been created successfully.";
                }
                // Call Put Method to Update Existing Supplier Details
                else
                {
                    response = await generateAPIResponse.SupplierViewRepo.Update("Supplier/" + model.SupplierID, model);
                    ViewBag.Message = "Supplier record has been updated successfully.";
                }

                if (response)
                {
                    ViewBag.Class = "text-success";
                    return RedirectToIndex();
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
            return View("Create", model);
        }
        #endregion

        #region [ Show Supplier Details ]
        /// <summary>
        /// Show Supplier Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            SupplierViewModel model = await generateAPIResponse.SupplierViewRepo.GetByID("supplier", id);
            if (model != null)
            {
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }
        #endregion
        public IActionResult GetSupplierDetails(int id)
        {
            SupplierViewModel model = generateAPIResponse.SupplierViewRepo.GetByID("supplier", id).Result;
            return Ok(model);
        }

        #region [ Delete Supplier Record form DB. ]
        /// <summary>
        /// Delete Supplier Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await generateAPIResponse.SupplierViewRepo.Delete("Supplier/" + id);

                if (response)
                {
                    ViewBag.Message = "Supplier record has been deleted successfully.";
                    return RedirectToIndex();
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