using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductSizeViewModel
    {
        public int ProductSizeID { get; set; }

        [Required(ErrorMessage = "Please Enter Product Size Name")]
        [DisplayName("Product Size Name")]
        [MaxLength(100)]
        public string ProductSizeName { get; set; }
    }
}
