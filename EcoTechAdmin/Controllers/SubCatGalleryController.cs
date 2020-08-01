#region [ Namespace ]
using BAL;
using BAL.ViewModels.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using System.Reflection;
using System.Linq;
#endregion

namespace EcoTechAdmin.Controllers
{
    public class SubCatGalleryController : AuthorizeController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;

        // Hosting Path Variable to Upload Files
        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public SubCatGalleryController(IUnitOfWork _generateAPIResponse, IWebHostEnvironment hostingEnvironment)
        {
            generateAPIResponse = _generateAPIResponse;
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

        #region [ Bind Partial View - Sub Category Gallery List baesd on the SubCategoryID: Called from Index.cshtml jQuery Ajax Call ]
        /// <summary>
        /// Bind Partial View - Sub Category Gallery List baesd on the SubCategoryID: Called from Index.cshtml jQuery Ajax Call
        /// </summary>
        /// <param name="id"></param> -- Sub Category ID
        /// <param name="catid"></param> -- Category ID
        /// <returns></returns>
        public async Task<IActionResult> BindList(int? id, int? catid)
        {
            string _apiMethod;
            if (id != null)
                _apiMethod = "subcatgallery/getbysubcategoryid/" + id;
            else
                _apiMethod = "subcatgallery";

            return PartialView("_SubCatGalList", await BindSubCatGalList(id, catid, _apiMethod));
        }
        #endregion

        #region [ Bind Sub Category Gallery List ]
        private async Task<IEnumerable<SubCatGalleryViewModel>> BindSubCatGalList(int? id, int? catid, string apiMethod)
        {
            var _subcatgallery = await generateAPIResponse.SubCatGalleryViewRepo.GetAll(apiMethod);
            GetCatIDSubCatID(id, catid);
            if (_subcatgallery != null && _subcatgallery.Count() > 0)
                TempData["LastOrderNo"] = _subcatgallery.Max(s => s.Order);
            else
                TempData.Remove("LastOrderNo");
            
            return _subcatgallery.OrderBy(subcat => subcat.Order);
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
            string apiMethod = "subcatgallery/setdefaultimage/" + id + "/" + subcatid;
            return PartialView("_SubCatGalList", await BindSubCatGalList(id, catid, apiMethod));
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
            IEnumerable<CategoryViewModel> _category = generateAPIResponse.CategoryViewRepo.GetAll("category").Result;
            if (_category != null)
            {
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
            IEnumerable<SubCategoryViewModel> _subcategory = await generateAPIResponse.SubCategoryViewRepo.GetAll("subcategory/getbycategoryid/" + id);
            if (_subcategory != null)
            {
                ViewBag.Categories = _subcategory;
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
                var response = generateAPIResponse.SubCatGalleryViewRepo.Save("subcatgallery", model).Result;

                if (response)
                {
                    TempData["Message"] = "Sub Category record has been created successfully.";
                    TempData["Class"] = "text-success";
                    return true;
                }
                else
                {
                    TempData["Message"] = "Something went wrong";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Something went wrong: " + ex.Message;
            }
            return false;
        }
        #endregion

        #region [ Delete Sub Category Record form DB ]
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
                if (await generateAPIResponse.SubCatGalleryViewRepo.Delete("subcatgallery/" + id))
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
                int _count = TempData["LastOrderNo"] != null ? (int)TempData["LastOrderNo"] + 1 : 1;
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

        #region [ Update Image Order ]
        /// <summary>
        /// Update Image Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("{controller=subcatgallery}/{action=updateorder}/{id?}/{orderno?}")]
        public async Task<IActionResult> UpdateOrder(int id, int orderno)
        {
            try
            {
                if (await generateAPIResponse.SubCatGalleryViewRepo.UpdateOrder("subcatgallery/updateorder", id, orderno))
                    return StatusCode(200, "Updated Successfull");
                else
                    return StatusCode(500);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Something went wrong: " + ex.Message;
                TempData["Message"] = "Something went wrong: " + ex.Message;
                return StatusCode(500);
            }
        }
        #endregion
    }
}