#region [ Namespace ]
using BAL;
using BAL.ViewModels.User;
using EcoTechAdmin.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
#endregion

namespace EcoTechAdmin.Controllers
{
    public class HomeController : Controller
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public HomeController(IUnitOfWork _generateAPIResponse)
        {
            generateAPIResponse = _generateAPIResponse;
        }
        #endregion

        public IActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                //var userName = HttpContext.User.Claims.First().Value; -- TO GET USER NAME FROM IDENTITY
                return RedirectToAction("Index", "Websites");
            }

            UserViewModel userViewModel = new UserViewModel();
            if(!String.IsNullOrEmpty(returnUrl))
                userViewModel.ReturnUrl = returnUrl;
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel _user)
        {
            String response = await generateAPIResponse.CommonRepo.GetStringContent("token/create/" + _user.UserName + "/" + _user.Password);
            if (!string.IsNullOrEmpty(response))
            {
                _user.Token = response.ToString();
                Authenticate(_user);

                if (Url.IsLocalUrl(_user.ReturnUrl))
                    return Redirect(_user.ReturnUrl);
                else
                    return RedirectToAction("Index", "Websites");
            }
            else
            {
                ViewBag.Message = "Invalid Username/Password. Please try again.";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Home");
        }

        private void Authenticate(UserViewModel _user)
        {
            
            var authUser = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim(ClaimTypes.Email, _user.UserName),
                new Claim("AuthToken", _user.Token),
            };

            var userIdentity = new ClaimsIdentity(authUser, "User Identity");

            var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

            HttpContext.SignInAsync(userPrincipal);
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
