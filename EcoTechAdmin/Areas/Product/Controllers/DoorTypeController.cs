using System.Collections.Generic;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace EcoTechAdmin.Areas.Product.Controllers
{
    public class DoorTypeController : ProductBaseController
    {
        public IActionResult Index()
        {
            IEnumerable<DoorTypeViewModel> doorTypes = new List<DoorTypeViewModel>();
            return View(doorTypes);
        }

        // Create New Door Type
        public IActionResult Create()
        {
            return View();
        }

        // Edit Door Type
        public IActionResult Edit(int id)
        {
            return View("Create");
        }

        // Edit Door Type
        [HttpPost]
        public IActionResult Edit(DoorTypeViewModel model)
        {
            return View("Create");
        }

        // Show Door Type Details
        public IActionResult Details(int id)
        {
            return View();
        }

        // Delete Door Type
        public IActionResult Delete(int id)
        {
            return View();
        }
    }
}