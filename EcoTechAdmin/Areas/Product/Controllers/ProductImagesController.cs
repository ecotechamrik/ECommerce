using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EcoTechAdmin.Areas.Product.Controllers
{
    public class ProductImagesController : ProductBaseController
    {
        public IActionResult Index(int? id)
        {
            return View();
        }
    }
}