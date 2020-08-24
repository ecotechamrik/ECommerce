using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductHeightViewModel
    {
        public int ProductHeightID { get; set; }

        [Required(ErrorMessage = "Please Enter Product Height Name")]
        [DisplayName("Product Height Name")]
        [MaxLength(100)]
        public string ProductHeightName { get; set; }
    }
}
