using BAL;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;

namespace EcoTechAdmin.Controllers
{
    public class SubCatGalleryController : AuthorizeController
    {
        #region [ Local Variables ]
        // Get API URL from appsettings.json
        Uri baseAddress = new Uri(Common.GetSectionString("APIAddress", ""));

        // HttpClient Variable to access the Web APIs
        HttpClient client;

        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region [ Default Constructor ]
        public SubCatGalleryController(IWebHostEnvironment hostingEnvironment)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region [ Index - Load Main/Index Page ]
        /// <summary>
        /// Index - Load the List of Sub Categories
        /// </summary>
        /// <returns></returns>
        [Route("{controller=subcatgallery}/{action=index}/{id?}/{catid?}")]
        public IActionResult Index(int? id, int? catid)
        {
            GetCatIDSubCatID(id, catid);
            return View();
        }
        #endregion

        #region [ Create ViewBag CategoryID, SubCategoryID from URL ]
        /// <summary>
        /// Create ViewBag CategoryID, SubCategoryID from URL
        /// </summary>
        /// <param name="id"></param>
        /// <param name="catid"></param>
        private void GetCatIDSubCatID(int? id, int? catid)
        {
            // SubCategoryID to load Sub Categories of the previously selected Category for BACK Button.
            if (id != null)
                ViewBag.SubCategoryID = id;

            // CategoryID to load Sub Categories of the previously selected Category for BACK Button.
            if (catid != null)
                ViewBag.CategoryID = catid;
        }
        #endregion

        #region [ Bind Partial View - Sub Category Gallery List baesd on the SubCategoryID ]
        /// <summary>
        /// Bind Partial View - Sub Category Gallery List baesd on the SubCategoryID
        /// </summary>
        /// <param name="id"></param> -- Sub Category ID
        /// <param name="catid"></param> -- Category ID
        /// <returns></returns>
        public async Task<IActionResult> BindList(int? id, int? catid)
        {
            var response = new HttpResponseMessage();
            if (id != null)
            {
                response = await client.GetAsync(client.BaseAddress + "subcatgallery/getbysubcategoryid/" + id);
            }
            else
                response = await client.GetAsync(client.BaseAddress + "subcatgallery");
            IEnumerable<SubCatGalleryViewModel> _subcatgallery = BindSubCatGalList(id, catid, response);

            return PartialView("_SubCatGalList", _subcatgallery);
        }
        #endregion

        #region [ Bind Sub Category Gallery List ]
        private IEnumerable<SubCatGalleryViewModel> BindSubCatGalList(int? id, int? catid, HttpResponseMessage response)
        {
            IEnumerable<SubCatGalleryViewModel> _subcatgallery = new List<SubCatGalleryViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _subcatgallery = JsonConvert.DeserializeObject<IEnumerable<SubCatGalleryViewModel>>(data);
            }
            GetCatIDSubCatID(id, catid);
            return _subcatgallery;
        }
        #endregion

        #region [ Set as Default/Main Image ]
        /// <summary>
        /// Set as Default/Main Image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subcatid"></param>
        /// <param name="catid"></param>
        /// <returns></returns>
        public async Task<IActionResult> SetDefaultImage(int? id, int? subcatid, int? catid)
        {
            var response = await client.GetAsync(client.BaseAddress + "subcatgallery/setdefaultimage/" + id + "/" + subcatid);
            IEnumerable<SubCatGalleryViewModel> _subcatgallery = BindSubCatGalList(id, catid, response);

            return PartialView("_SubCatGalList", _subcatgallery);
        }
        #endregion

        #region [ Create New Sub Category Data ]
        /// <summary>
        /// Create New Sub Category Data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{controller=subcatgallery}/{action=create}/{subcatid?}/{catid?}")]
        public IActionResult Create(int? subcatid, int? catid)
        {
            SubCatGalleryViewModel model = new SubCatGalleryViewModel { IsMainImage = true, SubCategoryID = subcatid, CategoryID = catid };
            GetCategories();
            //GetSubCategories(catid);
            GetCatIDSubCatID(subcatid, catid);
            return View(model);
        }
        #endregion

        #region [ Get All Categories - Bind Categories Drop Down List ]
        /// <summary>
        /// Get All Sub Categories - Bind Categories Drop Down List
        /// </summary>
        private void GetCategories()
        {
            var response = client.GetAsync(client.BaseAddress + "category").Result;
            IEnumerable<CategoryViewModel> _category = new List<CategoryViewModel>();
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _category = JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(data);
                ViewBag.Categories = _category;
            }
        }
        #endregion

        #region [ Bind Sub Categories ]
        /// <summary>
        /// Bind Sub Categories
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> bindsubcategories(int? id)
        {
            IEnumerable<SubCategoryViewModel> _subcategory = new List<SubCategoryViewModel>();

            var response = await client.GetAsync(client.BaseAddress + "subcategory/getbycategoryid/" + id);

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                _subcategory = JsonConvert.DeserializeObject<IEnumerable<SubCategoryViewModel>>(data);
            }
            return PartialView("_SubCategory", _subcategory);
        }
        #endregion

        #region [ Save & Update Sub Category Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Sub Category Details with Post & Put Methods of the Web APIs.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool SaveSubCategoryDetails(SubCatGalleryViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                var response = client.PostAsync(client.BaseAddress + "subcatgallery", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["Message"] = "Sub Category record has been created successfully.";
                    TempData["Class"] = "text-success";
                    return true;
                }
                else
                {
                    TempData["Message"] = "Something went wrong: " + response.ReasonPhrase;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Something went wrong: " + ex.Message;
            }
            return false;
        }
        #endregion

        #region [ Delete Sub Category Record form DB. ]
        /// <summary>
        /// Delete Sub Category Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{controller=subcatgallery}/{action=delete}/{id?}/{subcatid?}/{catid?}/{imageName?}")]
        public async Task<IActionResult> Delete(int id, int subcatid, int catid, string imageName)
        {
            try
            {
                var response = await client.DeleteAsync(client.BaseAddress + "subcatgallery/" + id);

                if (response.IsSuccessStatusCode)
                {
                    DeleteFilePath(imageName, subcatid);
                    TempData["Message"] = "Sub Category record has been deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Something went wrong: " + ex.Message;
            }
            return RedirectToAction("Index", new { id = subcatid, catid = catid });
        }
        #endregion

        #region [ Delete File with the Category ID and Sub Category ID ]
        /// <summary>
        /// Delete File with the Category ID and Sub Category ID
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="SubCategoryID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        private bool DeleteFilePath(string filename, int SubCategoryID)
        {
            string path = this._hostingEnvironment.WebRootPath + "\\Gallery\\" + SubCategoryID + "\\" + filename;

            // Delete Thumnail Image
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);

                // Delete Original Image
                path = path.Replace("T_", "O_");
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                return true;
            }
            else
                return false;
        }
        #endregion

        #region [ Upload Gallery Image ]
        /// <summary>
        /// Upload Gallery Image
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FileUpload(IList<IFormFile> files, IFormCollection data)
        {
            try
            {
                var CategoryID = Convert.ToInt32(data["CategoryID"]);
                var SubCategoryID = Convert.ToInt32(data["SubCategoryID"]);
                int _count = 1;
                foreach (IFormFile source in files)
                {
                    string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

                    filename = this.EnsureCorrectFilename(filename);

                    // "T_ for Thumbnail; O_ for Original
                    SubCatGalleryViewModel model = new SubCatGalleryViewModel { CategoryID = CategoryID, SubCategoryID = SubCategoryID, ThumbNailSizeImage = "T_" + filename, OriginalImage = "O_" + filename, IsMainImage = false, Order = _count };

                    if (SaveSubCategoryDetails(model))
                    {
                        // Save Thumnail Image
                        ResizeImage(source, GetPathAndFilename("T_" + filename, SubCategoryID));

                        // Save Original Image
                        using (FileStream output = System.IO.File.Create(GetPathAndFilename("O_" + filename, SubCategoryID)))
                        { 
                            await source.CopyToAsync(output); 
                        }
                    }
                    _count++;
                }

                return Ok("Uploaded");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region [ Resize the Image ]
        public void ResizeImage(IFormFile source, String _filePath)
        {
            // Load image.
            Image image = Image.FromStream(source.OpenReadStream());

            // Compute thumbnail size.
            Size thumbnailSize = GetThumbnailSize(image);

            // Get Thumbnail Image.
            image.GetThumbnailImage(thumbnailSize.Width, thumbnailSize.Height, () => false, IntPtr.Zero).Save(_filePath);
        }

        static Size GetThumbnailSize(Image original)
        {
            // Maximum size of any dimension.
            const int maxPixels = 40;

            // Width and height.
            int originalWidth = original.Width;
            int originalHeight = original.Height;

            // Compute best factor to scale entire image based on larger dimension.
            double factor;
            if (originalWidth > originalHeight)
            {
                factor = ((double)maxPixels / originalWidth) / 0.50;
            }
            else
            {
                factor = ((double)maxPixels / originalHeight) / 0.50;
            }

            // Return thumbnail size.
            return new Size((int)(originalWidth * factor), (int)(originalHeight * factor));
        }
        #endregion

        /// <summary>
        /// Get File name from the uploaded file.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string EnsureCorrectFilename(string filename)
        {
            if (filename.Contains("\\"))
                filename = filename.Substring(filename.LastIndexOf("\\") + 1);

            return filename;
        }

        /// <summary>
        /// Create Folder to the Upload [Gallery] Location with the Category ID and Sub Category ID
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="SubCategoryID"></param>
        /// <param name="CategoryID"></param>
        /// <returns></returns>
        private string GetPathAndFilename(string filename, int? SubCategoryID)
        {
            string path = this._hostingEnvironment.WebRootPath + "/Gallery/" + SubCategoryID + "/";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path + filename;
        }
        #endregion        
    }
}