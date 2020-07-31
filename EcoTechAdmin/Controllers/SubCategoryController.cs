#region [ Namespace ]
using BAL;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace EcoTechAdmin.Controllers
{
    public class SubCategoryController : AuthorizeController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;

        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public SubCategoryController(IUnitOfWork _generateAPIResponse, IWebHostEnvironment hostingEnvironment)
        {
            generateAPIResponse = _generateAPIResponse;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region [ Index - Load the List of Sub Categories ]
        /// <summary>
        /// Index - Load the List of Sub Categories
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            return await RedirectToIndex(id);
        }
        #endregion

        #region [ Load Index With Sub Category Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Sub Category Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex(int? id)
        {
            dynamic _subcategory;

            if (id != null)
            {
                _subcategory = await generateAPIResponse.SubCategoryViewRepo.GetAll("subcategory/getbycategoryid/" + id);
                ViewBag.CategoryID = id;
            }
            else
                _subcategory = await generateAPIResponse.SubCategoryViewRepo.GetAll("subcategory");

            return View("Index", _subcategory);
        }
        #endregion

        #region [ Create New Sub Category Data ]
        /// <summary>
        /// Create New Sub Category Data
        /// </summary>
        /// <returns></returns>
        public IActionResult Create(int? id)
        {
            SubCategoryViewModel model = new SubCategoryViewModel { IsActive = true, CategoryID = id };
            GetCategories();
            return View(model);
        }
        #endregion

        #region [ Get All Categories - Bind Categories Drop Down List ]
        /// <summary>
        /// Get All Categories - Bind Categories Drop Down List
        /// </summary>
        public void GetCategories()
        {
            IEnumerable<CategoryViewModel> _category = generateAPIResponse.CategoryViewRepo.GetAll("category").Result;
            if (_category != null)
            {
                ViewBag.Categories = _category;
            }
        }
        #endregion

        #region [ Save New Sub Category Data ]
        /// <summary>
        /// Save New Sub Category Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(SubCategoryViewModel model)
        {
            return await SaveSubCategoryDetails(model, "Create");
        }
        #endregion

        #region [ Edit Sub Category Data ]
        /// <summary>
        /// Edit Sub Category Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int id)
        {
            SubCategoryViewModel model = await generateAPIResponse.SubCategoryViewRepo.GetByID("subcategory", id);

            if (model != null)
            {
                GetCategories();
                return View("Create", model);
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Sub Category Data ]
        /// <summary>
        /// Update Sub Category Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(SubCategoryViewModel model)
        {
            return await SaveSubCategoryDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Sub Category Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Sub Category Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveSubCategoryDetails(SubCategoryViewModel model, String action)
        {
            try
            {
                var response = false;

                // Call Post Method to Create New Sub Category Details
                if (action.ToLower() == "create")
                {
                    response = await generateAPIResponse.SubCategoryViewRepo.Save("subcategory", model);
                    ViewBag.Message = "Sub Category record has been created successfully.";
                }
                // Call Put Method to Update Existing Sub Category Details
                else
                {
                    response = await generateAPIResponse.SubCategoryViewRepo.Update("subcategory/" + model.SubCategoryID, model);
                    ViewBag.Message = "Sub Category record has been updated successfully.";
                }

                if (response)
                {
                    ViewBag.Class = "text-success";
                    return await RedirectToIndex(null);
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
            GetCategories();
            return View("Create", model);
        }
        #endregion

        #region [ Show Sub Category Details ]
        /// <summary>
        /// Show Sub Category Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            SubCategoryViewModel model = await generateAPIResponse.SubCategoryViewRepo.GetByID("subcategory", id);
            if (model != null)
                return View(model);
            else
                return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Sub Category Record form DB ]
        /// <summary>
        /// Delete Sub Category Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (await generateAPIResponse.SubCategoryViewRepo.Delete("subcategory/" + id))
                {
                    await DeleteSubCatGallery(id);

                    ViewBag.Message = "Sub Category record has been deleted successfully.";

                    return await RedirectToIndex(null);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete All Sub Cate Gallery Images and Records from DB ]
        // Delete All Sub Cate Gallery Images and Records from DB

        public async Task<IActionResult> DeleteAllSubCatGallery(int id)
        {
            await DeleteSubCatGallery(id);
            TempData["Message"] = "All Gallery Images has been deleted successfully. Please upload new gallery now.";
            return Ok("Deleted");
        }

        private async Task DeleteSubCatGallery(int _subCategoryID)
        {
            await generateAPIResponse.CategoryViewRepo.Delete("subcatgallery/deletebysubcategoryid/" + _subCategoryID);

            string path = _hostingEnvironment.WebRootPath + "/Gallery/" + _subCategoryID;

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }            
        }
        #endregion
    }
}