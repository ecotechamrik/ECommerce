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
using Microsoft.Extensions.Logging;
#endregion

namespace EcoTechAdmin.Areas.Product.Controllers
{
    public class ProductImagesController : ProductBaseController
    {
        #region [ Local Variables ]
        // Generate API Response Variable through Dependency Injection
        IUnitOfWork generateAPIResponse;

        // Hosting Path Variable to Upload Files
        private readonly IWebHostEnvironment _hostingEnvironment;

        #endregion

        #region [ Default Constructor - Used to call Inject Dependency Injection Method for API Calls ]
        public ProductImagesController(IUnitOfWork _generateAPIResponse, IWebHostEnvironment hostingEnvironment)
        {
            generateAPIResponse = _generateAPIResponse;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        #region [ Index - Load Main/Index Page ]
        /// <summary>
        /// Index - Load the List of Product Images
        /// </summary>
        /// <returns></returns>
        [Route("[area]/[controller]/{action=index}/{id?}")]
        public IActionResult Index(int? id)
        {
            SetProductViewBagID(id);
            return View();
        }
        #endregion

        #region [ Set Product ID Into ViewBag ]
        private void SetProductViewBagID(int? id)
        {
            if (id != null)
                ViewBag.ProductID = id;
        }
        #endregion

        #region [ Bind Partial View - Product Images List baesd on the ProductID: Called from Index.cshtml jQuery Ajax Call ]
        /// <summary>
        /// Bind Partial View - Product Images List baesd on the ProductID: Called from Index.cshtml jQuery Ajax Call
        /// </summary>
        /// <param name="id"></param> -- Product ID
        /// <returns></returns>
        public async Task<IActionResult> BindList(int? id)
        {
            string _apiMethod;
            if (id != null)
                _apiMethod = "productimages/getimagesbyproductid/" + id;
            else
                _apiMethod = "productimages";

            return PartialView("_ProductImagesList", await BindProductImagesList(id, _apiMethod));
        }
        #endregion

        #region [ Bind Product Images List ]
        private async Task<IEnumerable<ProductImageViewModel>> BindProductImagesList(int? id, string apiMethod)
        {
            var _productimages = await generateAPIResponse.ProductImagesViewRepo.GetAll(apiMethod);
            SetProductViewBagID(id);
            if (_productimages != null && _productimages.Count() > 0)
                TempData["LastOrderNo"] = _productimages.Max(s => s.Order);
            else
                TempData.Remove("LastOrderNo");

            return _productimages.OrderBy(subcat => subcat.Order);
        }
        #endregion

        #region [ Set as Default/Main Image ]
        /// <summary>
        /// Set as Default/Main Image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productid"></param>
        /// <returns></returns>
        public async Task<IActionResult> SetDefaultImage(int? id, int? productid)
        {
            string apiMethod = "productimages/setdefaultimage/" + id + "/" + productid;
            return PartialView("_ProductImagesList", await BindProductImagesList(id, apiMethod));
        }
        #endregion

        #region [ Create New Product Images ]
        /// <summary>
        /// Create New Product Images
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[area]/[controller]/{action=create}/{productid?}")]
        public IActionResult Create(int? productid)
        {
            ProductImageViewModel model = new ProductImageViewModel { IsMainImage = true, ProductID = productid };
            SetProductViewBagID(productid);
            return View(model);
        }
        #endregion

        #region [ Save & Update Product Images Details with Post & Put Methods of the Web APIs ]
        /// <summary>
        /// Save & Update Product Images Details with Post & Put Methods of the Web APIs
        /// </summary>
        /// <param name="model"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private bool SaveProductImagesDetails(ProductImageViewModel model)
        {
            try
            {
                var response = generateAPIResponse.ProductImagesViewRepo.Save("productimages", model).Result;

                if (response)
                {
                    TempData["Message"] = "Product Images record has been created successfully.";
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

        #region [ Upload Product Image ]
        /// <summary>
        /// Upload Product Image
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestSizeLimit(int.MaxValue)]
        public async Task<IActionResult> FileUpload(IList<IFormFile> files, IFormCollection data)
        {
            var ProductID = Convert.ToInt32(data["ProductID"]);
            int _count = TempData["LastOrderNo"] != null ? (int)TempData["LastOrderNo"] + 1 : 1;
            
            foreach (IFormFile source in files)
            {
                string filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');

                filename = this.EnsureCorrectFilename(filename);

                // "T_ for Thumbnail; O_ for Original
                ProductImageViewModel model = new ProductImageViewModel { ProductID = ProductID, ThumbNailSizeImage = "T_" + filename, OriginalImage = "O_" + filename, IsMainImage = false, Order = _count };

                if (SaveProductImagesDetails(model))
                {
                    // Save Thumnail Image
                    ResizeImage(source, GetPathAndFilename("T_" + filename, ProductID));

                    // Save Original Image
                    using (FileStream output = System.IO.File.Create(GetPathAndFilename("O_" + filename, ProductID)))
                    {
                        await source.CopyToAsync(output);
                    }
                }
                _count++;
            }

            return Ok("Uploaded");            
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
        /// Create Folder to the Upload [Product Image] Location with the Product ID
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        private string GetPathAndFilename(string filename, int? ProductID)
        {
            string path = this._hostingEnvironment.WebRootPath + "/ProductImages/" + ProductID + "/";

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
        [Route("[area]/[controller]/{action=updateorder}/{id?}/{orderno?}")]
        public async Task<IActionResult> UpdateOrder(int id, int orderno)
        {
            try
            {
                if (await generateAPIResponse.ProductImagesViewRepo.UpdateOrder("productimages/updateorder", id, orderno))
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

        #region [ Delete Product Image Record form DB ]
        /// <summary>
        /// Delete Product Image Record form DB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("[area]/[controller]/{action=delete}/{id?}/{productid?}/{imageName?}")]
        public async Task<IActionResult> Delete(int id, int productid, string imageName)
        {
            try
            {
                if (await generateAPIResponse.ProductImagesViewRepo.Delete("productimages/" + id))
                {
                    DeleteFilePath(imageName, productid);
                    TempData["Message"] = "Product Image record has been deleted successfully.";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Something went wrong: " + ex.Message;
            }
            return RedirectToAction("Index", new { id = productid });
        }
        #endregion

        #region [ Delete File with the Product ID ]
        /// <summary>
        /// Delete File with the Product ID
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        private bool DeleteFilePath(string filename, int ProductID)
        {
            string path = this._hostingEnvironment.WebRootPath + "\\ProductImages\\" + ProductID + "\\" + filename;

            // Delete Thumnail Image
            if (System.IO.File.Exists(path))
            {
                System.IO.File.SetAttributes(path, FileAttributes.Normal);
                System.IO.File.Delete(path);

                // Delete Original Image
                path = path.Replace("T_", "O_");
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.SetAttributes(path, FileAttributes.Normal);
                    System.IO.File.Delete(path);
                }
                return true;
            }
            else
                return false;
        }
        #endregion

        #region [ Delete All Product Images and Records from DB ]
        // Delete All Product Images and Records from DB
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteAllProductImages(int id)
        {
            await generateAPIResponse.CategoryViewRepo.Delete("productimages/DeleteImagesByProductID/" + id);

            string path = _hostingEnvironment.WebRootPath + "/ProductImages/" + id;

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            TempData["Message"] = "All Product Images has been deleted successfully. Please upload new product images now.";
            return Ok("Deleted");
        }
        #endregion
    }
}