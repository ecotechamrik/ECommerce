﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EcoTechAdmin.Areas.Product.Controllers
{
    public class ProductDesignController : ProductBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}