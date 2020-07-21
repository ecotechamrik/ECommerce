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
using Microsoft.AspNetCore.Authorization;

namespace EcoTechAdmin.Controllers
{
    [Authorize]
    public class SectionController : Controller
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        Uri baseAddress = new Uri(Common.GetSectionString("APIAddress", ""));

        // HttpClient Variable to access the Web APIs
        HttpClient client;
        #endregion

        #region [ Default Constructor  ]
        public SectionController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
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
            var response = await client.GetAsync(client.BaseAddress + "section");
            IEnumerable<SectionViewModel> _section = new List<SectionViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _section = JsonConvert.DeserializeObject<IEnumerable<SectionViewModel>>(data);
            }
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
        public IActionResult Edit(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "section" + "/" + id).Result;
            SectionViewModel model = new SectionViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<SectionViewModel>(data);

                if (model != null)
                    return View("Create", model);
            }

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
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = new HttpResponseMessage();

                // Call Post Method to Create New Section Details
                if (action.ToLower() == "create")
                {
                    response = await client.PostAsync(client.BaseAddress + "section", content);
                    ViewBag.Message = "Section record has been created successfully.";
                }
                // Call Put Method to Update Existing Section Details
                else
                {
                    response = await client.PutAsync(client.BaseAddress + "section/" + model.SectionID, content);
                    ViewBag.Message = "Section record has been updated successfully.";
                }

                if (response.IsSuccessStatusCode)
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

        #region [ Show Section Details ]
        /// <summary>
        /// Show Section Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "section" + "/" + id).Result;
            SectionViewModel model = new SectionViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<SectionViewModel>(data);

                if (model != null)
                    return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Section Record form DB. ]
        /// <summary>
        /// Delete Section Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await client.DeleteAsync(client.BaseAddress + "section/" + id);

                if (response.IsSuccessStatusCode)
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