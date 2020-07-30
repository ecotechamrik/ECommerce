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

namespace EcoTechAdmin.Controllers
{
    public class CategoryController : AuthorizeController
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        Uri baseAddress = new Uri(Common.GetSectionString("APIAddress", ""));

        // HttpClient Variable to access the Web APIs
        HttpClient client;
        #endregion

        #region [ Default Constructor  ]
        public CategoryController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
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
            //var response = await client.GetAsync(client.BaseAddress + "category");
            var response = new HttpResponseMessage();
            if (id != null)
            {
                response = await client.GetAsync(client.BaseAddress + "category/getbysectionid/" + id);
                ViewBag.SectionID = id;
            }
            else
                response = await client.GetAsync(client.BaseAddress + "category");

            IEnumerable<CategoryViewModel> _category = new List<CategoryViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _category = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(data);
            }
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
            var response = client.GetAsync(client.BaseAddress + "section").Result;
            IEnumerable<SectionViewModel> _section = new List<SectionViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _section = JsonConvert.DeserializeObject<IEnumerable<SectionViewModel>>(data);
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
        public IActionResult Edit(int? id, int? sectionid)
        {
            var response = client.GetAsync(client.BaseAddress + "category" + "/" + id).Result;
            CategoryViewModel model = new CategoryViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<CategoryViewModel>(data);

                if (model != null)
                {
                    GetSections();
                    ViewBag.SectionID = model.SectionID;
                    return View("Create", model);
                }
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
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = new HttpResponseMessage();

                // Call Post Method to Create New Category Details
                if (action.ToLower() == "create")
                {
                    response = await client.PostAsync(client.BaseAddress + "category", content);
                    ViewBag.Message = "Category record has been created successfully.";
                }
                // Call Put Method to Update Existing Category Details
                else
                {
                    response = await client.PutAsync(client.BaseAddress + "category/" + model.CategoryID, content);
                    ViewBag.Message = "Category record has been updated successfully.";
                }

                if (response.IsSuccessStatusCode)
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
                ViewBag.Message = "Something went wrong: " + ex.Message;
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
        public IActionResult Details(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "category" + "/" + id).Result;
            CategoryViewModel model = new CategoryViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<CategoryViewModel>(data);

                if (model != null)
                {
                    ViewBag.SectionID = model.SectionID;
                    return View(model);
                }
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Category Record form DB. ]
        /// <summary>
        /// Delete Category Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await client.DeleteAsync(client.BaseAddress + "category/" + id);

                if (response.IsSuccessStatusCode)
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