using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EcoTechAdmin.Areas.Product.Controllers
{
    [Area("Product")]
    public class DoorTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}