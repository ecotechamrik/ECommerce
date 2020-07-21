using BAL;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EcoTechAdmin.Controllers
{
    [Authorize]
    public class WebsitesController : Controller
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        Uri baseAddress = new Uri(Common.GetSectionString("APIAddress", ""));

        // HttpClient Variable to access the Web APIs
        HttpClient client;
        #endregion

        #region [ Default Constructor  ]
        public WebsitesController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }
        #endregion

        #region [ Index - Load the list of websites ]
        /// <summary>
        /// Index - Load the list of websites
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get All Websites List from All Properties of the website
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<WebsiteInfoViewModel>> GetAllWebsiteDataAsync(String _search)
        {
            IEnumerable<WebsiteInfoViewModel> _websites = new List<WebsiteInfoViewModel>();

            var response = await client.GetAsync(client.BaseAddress + "website");
            //var response1 = client.GetAsync(client.BaseAddress + "website").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _websites = JsonConvert.DeserializeObject<IEnumerable<WebsiteInfoViewModel>>(data);
            }

            if (!String.IsNullOrEmpty(_search))
            {
                // Search website details into all properties
                _websites = _websites.Where(s =>
                                          (s.WebsiteName != null ? s.WebsiteName.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.WebsiteBannerTitle != null ? s.WebsiteBannerTitle.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.WebsiteBannerTagLine != null ? s.WebsiteBannerTagLine.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.CompanyName != null ? s.CompanyName.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.CompanyDesc != null ? s.CompanyDesc.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.ContactEmailID != null ? s.ContactEmailID.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.Cell != null ? s.Cell.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.OfficePhone != null ? s.OfficePhone.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.Fax != null ? s.Fax.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.DevelopedBy != null ? s.DevelopedBy.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.Address != null ? s.Address.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.URL != null ? s.URL.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.CpanelUser != null ? s.CpanelUser.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.FTPUser != null ? s.FTPUser.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.DBUser != null ? s.DBUser.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.AdminUser != null ? s.AdminUser.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.HostingProviderName != null ? s.HostingProviderName.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.HostingProviderDesc != null ? s.HostingProviderDesc.ToLower().Contains(_search.ToLower()) : false)
                                       || (s.HostingProviderContactNo != null ? s.HostingProviderContactNo.ToLower().Contains(_search.ToLower()) : false)
                                       ).ToList();
            }
            return _websites;
        }
        #endregion

        #region [ Search the particular website ]
        /// <summary>
        /// Search the selected website
        /// </summary>
        /// <param name="currentFilter"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<IActionResult> Search(String currentFilter, String search)
        {
            if (search == null && !string.IsNullOrEmpty(currentFilter))
            {
                search = currentFilter;
            }
            ViewData["CurrentFilter"] = search;
            return PartialView("_WebsitesList", await SearchResult(search));
        }

        /// <summary>
        /// Search Website Results
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        private async Task<IEnumerable<WebsiteInfoViewModel>> SearchResult(string search)
        {
            var searchResults = await GetAllWebsiteDataAsync(search);
            if (searchResults.Count() > 0)
                ViewBag.HaveRecords = true;
            else
                ViewBag.HaveRecords = false;
            return searchResults;
        }
        #endregion

        #region [ Create New Website Data ]
        /// <summary>
        /// Create New Website Data
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Website Data ]
        /// <summary>
        /// Save New Website Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(WebsiteInfoViewModel model)
        {
            return await SaveWebsiteDetails(model, "Create");
        }
        #endregion

        #region [ Edit Website Data ]
        /// <summary>
        /// Edit Website Data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "website" + "/" + id).Result;
            WebsiteInfoViewModel model = new WebsiteInfoViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<WebsiteInfoViewModel>(data);

                if (model != null)
                    return View("Create", model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Website Data ]
        /// <summary>
        /// Update Website Data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(WebsiteInfoViewModel model)
        {
            return await SaveWebsiteDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update website Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update website Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveWebsiteDetails(WebsiteInfoViewModel model, String action)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = new HttpResponseMessage();

                // Call Post Method to Create New Website Details
                if (action.ToLower() == "create")
                {
                    response = await client.PostAsync(client.BaseAddress + "website", content);
                }
                // Call Put Method to Update Existing Website Details
                else
                {
                    response = await client.PutAsync(client.BaseAddress + "website/" + model.WebsiteID, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Website record has been updated successfully.";
                    ViewBag.Class = "text-success";
                    return View("Index");
                }
                else
                {
                    return Json(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            return View("Create", model);
        }
        #endregion

        #region [ Show Website Details ]
        /// <summary>
        /// Show Website Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "website" + "/" + id).Result;
            WebsiteInfoViewModel model = new WebsiteInfoViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<WebsiteInfoViewModel>(data);

                if (model != null)
                    return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Website Record form DB. ]
        /// <summary>
        /// Delete Website Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var response = await client.DeleteAsync(client.BaseAddress + "website/" + id);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Website record has been deleted successfully.";
                    //return PartialView("_Websites", await SearchResult(""));
                    return Ok(ViewBag.Message);

                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region [ Load Website Main Page Partial View ]
        /// <summary>
        /// Load Website Main Page Partial View
        /// </summary>
        /// <returns></returns>
        public IActionResult WebsitesMainPage()
        {
            return PartialView("_Websites", SearchResult(""));
        }
        #endregion
    }
}