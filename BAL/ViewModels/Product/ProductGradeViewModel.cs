using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductGradeViewModel
    {
        public int ProductGradeID { get; set; }

        [Required(ErrorMessage = "Please Enter Product Grade Name")]
        [DisplayName("Product Grade Name")]
        [MaxLength(100)]
        public string ProductGradeName { get; set; }
    }
}
