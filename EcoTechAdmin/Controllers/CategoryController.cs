#region [ Namespace References ]
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
    public class CategoryController : AuthorizeController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public CategoryController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        #region [ Index - Load the List of Categories ]
        /// <summary>
        /// Index - Load the List of Categories
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                TempData["SectionID"] = id;
            }
            GetSections();
            return await RedirectToIndex(id);
        }
        #endregion

        #region [ Load Index With Category Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Category Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex(int? id)
        {
            IEnumerable<CategoryViewModel> _category = new List<CategoryViewModel>();

            if (id != null)
            {
                _category = await generateAPIResponse.CategoryViewRepo.GetAll("category/getbysectionid/" + id);
                ViewBag.SectionID = id;
            }
            else
                _category = await generateAPIResponse.CategoryViewRepo.GetAll("category");

            TempData["lastCatOrder"] = _category.OrderByDescending(x => x.CategoryOrder).Take(1).Select(c => c.CategoryOrder).FirstOrDefault() + 1;
            return View("Index", _category.OrderBy(c => c.CategoryOrder));
        }
        #endregion

        #region [ Create New Category Data ]
        /// <summary>
        /// Create New Category Data
        /// </summary>
        /// <returns></returns>
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewBag.SectionID = id;
            }

            CategoryViewModel model = new CategoryViewModel();
            model.IsActive = true;
            if (TempData["lastCatOrder"] != null)
            {
                model.CategoryOrder = (int)TempData["lastCatOrder"];
            }
            GetSections();
            return View(model);
        }
        #endregion

        #region [ Get All Sections - Bind Sections Drop Down List ]
        /// <summary>
        /// Get All Categories - Bind Categories Drop Down List
        /// </summary>
        private void GetSections()
        {
            IEnumerable<SectionViewModel> _section = generateAPIResponse.SectionViewRepo.GetAll("section").Result;

            if (_section != null)
            {
                ViewBag.Sections = _section;
            }
        }
        #endregion

        #region [ Save New Category Data ]
        /// <summary>
        /// Save New Category Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CategoryViewModel model)
        {
            return await SaveCategoryDetails(model, "Create");
        }
        #endregion

        #region [ Edit Category Data ]
        /// <summary>
        /// Edit Category Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id, int? sectionid)
        {
            CategoryViewModel model = await generateAPIResponse.CategoryViewRepo.GetByID("category", id);
            if (model != null)
            {
                GetSections();
                ViewBag.SectionID = model.SectionID;
                return View("Create", model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Category Data ]
        /// <summary>
        /// Update Category Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryViewModel model)
        {
            return await SaveCategoryDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Category Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Category Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveCategoryDetails(CategoryViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Category Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.CategoryViewRepo.Save("category", model);
                    ViewBag.Message = "Category record has been created successfully.";
                }
                // Call Put Method to Update Existing Category Details
                else
                {
                    response = await generateAPIResponse.CategoryViewRepo.Update("category/" + model.CategoryID, model);
                    ViewBag.Message = "Category record has been updated successfully.";
                }

                if (response)
                {
                    ViewBag.Class = "text-success";
                    GetSections();
                    return await RedirectToIndex(model.SectionID);
                }
                else
                {
                    ViewBag.Message = null;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something Went Wrong: " + ex.Message;
            }
            GetSections();
            return View("Create", model);
        }
        #endregion

        #region [ Show Category Details ]
        /// <summary>
        /// Show Category Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            CategoryViewModel model = await generateAPIResponse.CategoryViewRepo.GetByID("category", id);
            if (model != null)
            {
                ViewBag.SectionID = model.SectionID;
                return View(model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Category Record form DB ]
        /// <summary>
        /// Delete Category Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await generateAPIResponse.CategoryViewRepo.Delete("category/" + id))
                {
                    ViewBag.Message = "Category record has been deleted successfully.";
                    GetSections();
                    int? sectionID = null;
                    if (TempData["SectionID"] != null)
                        sectionID = (int)TempData["SectionID"];
                    return await RedirectToIndex(sectionID);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            GetSections();
            return RedirectToAction("Index");
        }
        #endregion
    }
}