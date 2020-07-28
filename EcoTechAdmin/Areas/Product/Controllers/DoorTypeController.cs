using System.Collections.Generic;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;

namespace EcoTechAdmin.Areas.Product.Controllers
{
    [Area("Product")]
    public class DoorTypeController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<DoorTypeViewModel> doorTypes = new List<DoorTypeViewModel>();
            return View(doorTypes);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View("Create");
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}