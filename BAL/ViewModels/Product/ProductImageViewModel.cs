using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BAL.ViewModels.Product
{
    public class ProductImageViewModel
    {        
        [Display(Name = "Product Image ID")]
        public int ProductImageID { get; set; }

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

        [Display(Name = "Product ID")]
        public int? ProductID { get; set; }

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        public IFormFile files { set; get; }        
    }
}
