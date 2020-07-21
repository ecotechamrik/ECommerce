using BAL;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EcoTechAdmin.Controllers
{
    [Authorize]
    public class SubCatGalleryController : Controller
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        Uri baseAddress = new Uri(Common.GetSectionString("APIAddress", ""));

        // HttpClient Variable to access the Web APIs
        HttpClient client;
        #endregion

        #region [ Default Constructor  ]
        public SubCatGalleryController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        #endregion

        #region [ Index - Load the List of Sub Categories ]
        /// <summary>
        /// Index - Load the List of Sub Categories
        /// </summary>
        /// <returns></returns>
        [Route("{controller=subcatgallery}/{action=index}/{id?}/{catid?}")]
        public IActionResult Index(int? id, int? catid)
        {
            GetCatIDSubCatID(id, catid);

            return RedirectToIndex(id);
        }
        #endregion

        #region [ Create ViewBag CategoryID, SubCategoryID from URL ]
        /// <summary>
        /// Create ViewBag CategoryID, SubCategoryID from URL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="catid"></param>
        private void GetCatIDSubCatID(int? id, int? catid)
        {
            // SubCategoryID to load Sub Categories of the previously selected Category for BACK Button.
            if (id != null)
                ViewBag.SubCategoryID = id;

            // CategoryID to load Sub Categories of the previously selected Category for BACK Button.
            if (catid != null)
                ViewBag.CategoryID = catid;
        }
        #endregion

        #region [ Load Index With Sub Category Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Sub Category Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private IActionResult RedirectToIndex(int? id)
        {
            var response = new HttpResponseMessage();
            if (id != null)
            {
                response = client.GetAsync(client.BaseAddress + "subcatgallery/getbysubcategoryid/" + id).Result;
            }
            else
                response = client.GetAsync(client.BaseAddress + "subcatgallery").Result;

            IEnumerable<SubCatGalleryViewModel> _subcatgallery = new List<SubCatGalleryViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _subcatgallery = JsonConvert.DeserializeObject<IEnumerable<SubCatGalleryViewModel>>(data);
            }
            return View("Index", _subcatgallery);
        }
        #endregion

        #region [ Create New Sub Category Data ]
        /// <summary>
        /// Create New Sub Category Data
        /// </summary>
        /// <returns></returns>
        [Route("{controller=subcatgallery}/{action=create}/{subcatid?}/{catid?}")]
        public IActionResult Create(int? subcatid, int? catid)
        {
            SubCatGalleryViewModel model = new SubCatGalleryViewModel { IsMainImage = true, SubCategoryID = subcatid, CategoryID = catid };
            GetCategories();
            GetSubCategories();
            GetCatIDSubCatID(subcatid, catid);
            return View(model);
        }
        #endregion

        #region [ Get All Categories - Bind Categories Drop Down List ]
        /// <summary>
        /// Get All Sub Categories - Bind Categories Drop Down List
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

        #region [ Get All Sub Categories - Bind Categories Drop Down List ]
        /// <summary>
        /// Get All Sub Categories - Bind Categories Drop Down List
        /// </summary>
        private void GetSubCategories()
        {
            var response = client.GetAsync(client.BaseAddress + "subcategory").Result;
            IEnumerable<SubCategoryViewModel> _subcategory = new List<SubCategoryViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _subcategory = JsonConvert.DeserializeObject<IEnumerable<SubCategoryViewModel>>(data);
                ViewBag.SubCategories = _subcategory;
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
        public async Task<IActionResult> Create(SubCatGalleryViewModel model)
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
        [Route("{controller=subcatgallery}/{action=edit}/{id?}/{subcatid?}/{catid?}")]
        public IActionResult Edit(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "subcategory" + "/" + id).Result;
            SubCatGalleryViewModel model = new SubCatGalleryViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<SubCatGalleryViewModel>(data);

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
        public async Task<IActionResult> Edit(SubCatGalleryViewModel model)
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
        private async Task<IActionResult> SaveSubCategoryDetails(SubCatGalleryViewModel model, String action)
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
                    return RedirectToIndex(null);
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
            SubCatGalleryViewModel model = new SubCatGalleryViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<SubCatGalleryViewModel>(data);

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
                    ViewBag.Message = "Sub Category record has been deleted successfully.";
                    return RedirectToIndex(null);

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