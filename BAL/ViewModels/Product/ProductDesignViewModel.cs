using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductDesignViewModel
    {
        public int ProductDesignID { get; set; }

        [Required(ErrorMessage = "Please Enter Product Design Name")]
        [DisplayName("Product Design Name")]
        [MaxLength(100)]
        public string ProductDesignName { get; set; }
    }
}
