using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductThicknessViewModel
    {
        public int ProductThicknessID { get; set; }

        [Required(ErrorMessage = "Please Enter Thickness Name")]
        [DisplayName("Product Thickness Name")]
        [MaxLength(100)]
        public string ProductThicknessName { get; set; }
        public string ProductCodeInitials { get; set; }
        public int ProductAttributeThicknessID { get; set; }
        public bool Active { get; set; }
    }
}
