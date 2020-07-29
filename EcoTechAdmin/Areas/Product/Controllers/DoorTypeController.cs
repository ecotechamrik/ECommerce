#region [ Namespace References ]
using System.Collections.Generic;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;
#endregion

namespace EcoTechAdmin.Areas.Product.Controllers
{
    public class DoorTypeController : ProductBaseController
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        IConfiguration config;

        // HttpClient Variable to access the Web APIs
        HttpClient client;
        #endregion

        #region [ Default Constructor  ]
        public DoorTypeController(IConfiguration _config)
        {
            client = new HttpClient();
            config = _config;
            client.BaseAddress = new Uri(config["URL:API"]);
        }
        #endregion

        #region [ Index - Load the List of Door Types ]
        /// <summary>
        /// Index - Load the List of Door Types
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return await RedirectToIndex();
        }
        #endregion

        #region [ Load Index With Door Types Listing based on given Response from API ]
        /// <summary>
        /// Load Index With Door Types Listing based on given Response from API
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<IActionResult> RedirectToIndex()
        {
            var response = await client.GetAsync(client.BaseAddress + "doortypes");
            IEnumerable<DoorTypeViewModel> _doorTypes = new List<DoorTypeViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _doorTypes = JsonConvert.DeserializeObject<IEnumerable<DoorTypeViewModel>>(data);
            }
            return View("Index", _doorTypes);
        }
        #endregion

        #region [ Create New Door Type ]
        /// <summary>
        /// Create New Door Type
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region [ Save New Door Type ]
        /// <summary>
        /// Save New Door Type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(DoorTypeViewModel model)
        {
            return await SaveDoorTypeDetails(model, "Create");
        }
        #endregion

        #region [ Edit Door Type ]
        /// <summary>
        /// Edit Door Type
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "doortypes" + "/" + id).Result;
            DoorTypeViewModel model = new DoorTypeViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<DoorTypeViewModel>(data);

                if (model != null)
                    return View("Create", model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region [ Update Door Type ]
        /// <summary>
        /// Update Door Type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(DoorTypeViewModel model)
        {
            return await SaveDoorTypeDetails(model, "Edit");
        }
        #endregion

        #region [ Save & Update Door Type Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Door Type Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private async Task<IActionResult> SaveDoorTypeDetails(DoorTypeViewModel model, String action)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = new HttpResponseMessage();

                // Call Post Method to Create New Door Type Details
                if (action.ToLower() == "create")
                {
                    response = await client.PostAsync(client.BaseAddress + "doortypes", content);
                    ViewBag.Message = "Door Type record has been created successfully.";
                }
                // Call Put Method to Update Existing Door Type Details
                else
                {
                    response = await client.PutAsync(client.BaseAddress + "doortypes/" + model.DoorTypeID, content);
                    ViewBag.Message = "Door Type record has been updated successfully.";
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

        #region [ Show Door Type Details ]
        /// <summary>
        /// Show Door Type Details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "doortypes" + "/" + id).Result;
            DoorTypeViewModel model = new DoorTypeViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                model = JsonConvert.DeserializeObject<DoorTypeViewModel>(data);

                if (model != null)
                    return View(model);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region [ Delete Door Type Record form DB. ]
        /// <summary>
        /// Delete Door Type Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await client.DeleteAsync(client.BaseAddress + "doortypes/" + id);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Door Type record has been deleted successfully.";
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