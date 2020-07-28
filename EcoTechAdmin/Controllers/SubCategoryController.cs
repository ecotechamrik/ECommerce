using BAL;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EcoTechAdmin.Controllers
{
    public class SubCategoryController : AuthorizeController
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        Uri baseAddress = new Uri(Common.GetSectionString("APIAddress", ""));

        // HttpClient Variable to access the Web APIs
        HttpClient client;

        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region [ Default Constructor  ]
        public SubCategoryController(IWebHostEnvironment hostingEnvironment)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
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
            var response = new HttpResponseMessage();
            if (id != null)
            {
                response = await client.GetAsync(client.BaseAddress + "subcategory/getbycategoryid/" + id);
                ViewBag.CategoryID = id;
            }
            else
                response = await client.GetAsync(client.BaseAddress + "subcategory");

            IEnumerable<SubCategoryViewModel> _subcategory = new List<SubCategoryViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _subcategory = JsonConvert.DeserializeObject<IEnumerable<SubCategoryViewModel>>(data);
            }
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
        private void GetCategories()
        {
            var response = client.GetAsync(client.BaseAddress + "category").Result;
            IEnumerable<CategoryViewModel> _category = new List<CategoryViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _category = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(data);
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
        public IActionResult Edit(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "subcategory" + "/" + id).Result;
            SubCategoryViewModel model = new SubCategoryViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<SubCategoryViewModel>(data);

                if (model != null)
                {
                    GetCategories();
                    return View("Create", model);
                }
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
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = new HttpResponseMessage();

                // Call Post Method to Create New Sub Category Details
                if (action.ToLower() == "create")
                {
                    response = await client.PostAsync(client.BaseAddress + "subcategory", content);
                    ViewBag.Message = "Sub Category record has been created successfully.";
                }
                // Call Put Method to Update Existing Sub Category Details
                else
                {
                    response = await client.PutAsync(client.BaseAddress + "subcategory/" + model.SubCategoryID, content);
                    ViewBag.Message = "Sub Category record has been updated successfully.";
                }

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Class = "text-success";
                    return await RedirectToIndex(null);
                }
                else
                {
                    ViewBag.Message = null;
                    GetCategories();
                    return View("Create", model);
                }
            }
            catch (Exception ex)
            {
                GetCategories();
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
        public IActionResult Details(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "subcategory" + "/" + id).Result;
            SubCategoryViewModel model = new SubCategoryViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<SubCategoryViewModel>(data);

                if (model != null)
                    return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Sub Category Record form DB. ]
        /// <summary>
        /// Delete Sub Category Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await client.DeleteAsync(client.BaseAddress + "subcategory/" + id);

                if (response.IsSuccessStatusCode)
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
            await client.DeleteAsync(client.BaseAddress + "subcatgallery/deletebysubcategoryid/" + _subCategoryID);

            string path = this._hostingEnvironment.WebRootPath + "/Gallery/" + _subCategoryID;

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }            
        }
        #endregion
    }
}