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
        Uri baseAddress = new Uri(Common.GetSectionString("APIAddress", ""));
        HttpClient client;

        public WebsitesController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public IActionResult Index()
        {
            List<WebsiteInfoViewModel> _websites = new List<WebsiteInfoViewModel>();
            var response = client.GetAsync(client.BaseAddress + "website").Result; 
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _websites = JsonConvert.DeserializeObject<List<WebsiteInfoViewModel>>(data);
            }
            return View(_websites);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(WebsiteInfoViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = client.PostAsync(client.BaseAddress + "website", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            var response = client.GetAsync(client.BaseAddress + "website" + "/" + id).Result;
            WebsiteInfoViewModel _website = new WebsiteInfoViewModel();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;

                if (data != "" && data.StartsWith('['))
                    data = data.Substring(1, data.Length - 2);
                _website = JsonConvert.DeserializeObject<WebsiteInfoViewModel>(data);
                
                if(_website!=null)
                    return View("Create", _website);
            }

            return View("Index");
        }
    }
}