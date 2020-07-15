using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EcoTechAdmin.Models;

namespace EcoTechAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region [ 404 - Page Not Found ]
        /// <summary>
        /// 404 - Page Not Found
        /// </summary>
        /// <returns></returns>

        [Route("Home/404")]
        public IActionResult PageNotFound()
        {
            string originalPath = "unknown";
            if (HttpContext.Items.ContainsKey("originalPath"))
            {
                originalPath = HttpContext.Items["originalPath"] as string;
            }
            ViewData["ErrorMessage"] = originalPath;
            return View();
        }

        /// <summary>
        /// 404 - Page Not Found
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [Route("Home/404/{code:int}")]
        public IActionResult PageNotFound(int code)
        {
            ViewData["ErrorMessage"] = $"Error occured. The ErrorCode is: {code}";
            return View();
        }
        #endregion
    }
}
