using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductWidthViewModel
    {
        public int ProductWidthID { get; set; }

        [Required(ErrorMessage = "Please Enter Width Name")]
        [DisplayName("Product Width Name")]
        [MaxLength(100)]
        public string ProductWidthName { get; set; }

        [DisplayName("Width")]
        public string DisplayProductWidthName { get; set; }
    }
}
