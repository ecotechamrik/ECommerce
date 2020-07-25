using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BAL.ViewModels.Product
{
    public class SubCatGalleryViewModel
    {        
        [Display(Name = "Sub Category ID")]
        public int SubCatGalleryID { get; set; }

        [Display(Name = "Thumnail Size Image")]
        public string ThumbNailSizeImage { get; set; }

        [Display(Name = "Original Image")]
        public string OriginalImage { get; set; }

        [Display(Name = "Order")]
        public int Order { get; set; }

        [Display(Name = "Main Image")]
        public bool IsMainImage { get; set; }

        [JsonIgnore]
        [Display(Name = "Main Image")]
        public string MainImage
        {
            get { return IsMainImage == true ? "Yes" : "No"; }
        }

        [Display(Name = "Category ID")]
        public int? CategoryID { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Sub Category ID")]
        public int? SubCategoryID { get; set; }

        [Display(Name = "Sub Category Name")]
        public string SubCategoryName { get; set; }

        public IFormFile files { set; get; }        
    }
}
