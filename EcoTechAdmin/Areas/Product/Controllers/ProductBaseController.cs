using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EcoTechAdmin.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoTechAdmin.Areas.Product.Controllers
{
    [Area("Product")]
    public class ProductBaseController : AuthorizeController
    {
        
    }
}