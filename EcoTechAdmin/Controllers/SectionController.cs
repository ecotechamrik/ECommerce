#region [ Namespace ]
using BAL;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
#endregion

namespace EcoTechAdmin.Controllers
{
    public class SectionController : AuthorizeController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public SectionController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Sections ]
        /// <summary>
        /// Index - Load the List of Sections
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Section Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Section Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex()
        {
            IEnumerable<SectionViewModel> _section = await generateAPIResponse.SectionViewRepo.GetAll("section");
            return View("Index", _section.OrderBy(c => c.SectionOrder));
        }
        #endregion

        #region [ Create New Section Data ]
        /// <summary>
        /// Create New Section Data
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            SectionViewModel model = new SectionViewModel();
            model.IsActive = true;
            return View(model);
        }
        #endregion

        #region [ Save New Section Data ]
        /// <summary>
        /// Save New Section Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(SectionViewModel model)
        {
            return await SaveSectionDetails(model, "Create");
        }
        #endregion

        #region [ Edit Section Data ]
        /// <summary>
        /// Edit Section Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            SectionViewModel model = await generateAPIResponse.SectionViewRepo.GetByID("section", id);
            if (model != null)
                return View("Create", model);
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Section Data ]
        /// <summary>
        /// Update Section Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(SectionViewModel model)
        {
            return await SaveSectionDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Section Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Section Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveSectionDetails(SectionViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Section Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.SectionViewRepo.Save("section", model);
                    ViewBag.Message = "Section record has been created successfully.";
                }
                // Call Put Method to Update Existing Section Details
                else
                {
                    response = await generateAPIResponse.SectionViewRepo.Update("section/" + model.SectionID, model);
                    ViewBag.Message = "Section record has been updated successfully.";
                }

                if (response)
                {
                    ViewBag.Class = "text-success";
                    return await RedirectToIndex();
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

        #region [ Show Section Details ]
        /// <summary>
        /// Show Section Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            SectionViewModel model = await generateAPIResponse.SectionViewRepo.GetByID("section", id);
            if (model != null)
                return View(model);
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Section Record form DB ]
        /// <summary>
        /// Delete Section Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await generateAPIResponse.SectionViewRepo.Delete("section/" + id);

                if (response)
                {
                    ViewBag.Message = "Section record has been deleted successfully.";
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