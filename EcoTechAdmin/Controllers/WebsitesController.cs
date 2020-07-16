using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BAL;
using System.Net.Http;
using BAL.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace EcoTechAdmin.Controllers
{
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
        /// Load the list of websites
        /// </summary>
        /// <returns></returns>
        //public IActionResult Index()
        //{
        //    return GetAllWebsiteData("Index");
        //}

        public IActionResult Index(string currentFilter, string search)
        {
            //ViewData["CurrentSort"] = sortOrder;
            //ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";

            if (search == null && !string.IsNullOrEmpty(currentFilter))
            {
                search = currentFilter;
            }
            ViewData["CurrentFilter"] = search;

            List<WebsiteInfoViewModel> _websites = new List<WebsiteInfoViewModel>();
            var response = client.GetAsync(client.BaseAddress + "website").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _websites = JsonConvert.DeserializeObject<List<WebsiteInfoViewModel>>(data);
            }

            if (!String.IsNullOrEmpty(search))
            {
                _websites = _websites.Where(s => s.WebsiteName.ToLower().Contains(search.ToLower())
                                       || s.WebsiteBannerTitle.ToLower().Contains(search.ToLower())
                                       || s.WebsiteBannerTitle.ToLower().Contains(search.ToLower())
                                       || s.WebsiteBannerTagLine.ToLower().Contains(search.ToLower())).ToList();
            }
            return View(_websites);
        }

        /// <summary>
        /// Get All Websites List
        /// </summary>
        /// <returns></returns>
        private IActionResult GetAllWebsiteData(String _view)
        {
            List<WebsiteInfoViewModel> _websites = new List<WebsiteInfoViewModel>();
            var response = client.GetAsync(client.BaseAddress + "website").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _websites = JsonConvert.DeserializeObject<List<WebsiteInfoViewModel>>(data);
            }

            return View(_view, _websites);
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
        public IActionResult Create(WebsiteInfoViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = client.PostAsync(client.BaseAddress + "website", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Website record has been created successfully.";
                    ViewBag.Class = "text-success";
                    return GetAllWebsiteData("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            return View();
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
        public IActionResult Edit(WebsiteInfoViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = client.PutAsync(client.BaseAddress + "website/" + model.WebsiteID, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Website record has been updated successfully.";
                    ViewBag.Class = "text-success";
                    return GetAllWebsiteData("Index");
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
        public IActionResult Delete(int id)
        {
            try
            {
                var response = client.DeleteAsync(client.BaseAddress + "website/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Website record has been deleted successfully.";
                    return GetAllWebsiteData("Index");
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