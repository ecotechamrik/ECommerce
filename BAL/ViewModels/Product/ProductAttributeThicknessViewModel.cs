using BAL.ViewModels.Common;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductAttributeThicknessViewModel
    {
        public int ProductAttributeThicknessID { get; set; }
        public int? ProductAttributeID { get; set; }
        public int ProductThicknessID { get; set; }

        [DisplayName("Thickness")]
        [MaxLength(100)]
        public string ProductThicknessName { get; set; }
        
        [DisplayName("Key")]
        public string ProductCodeInitials { get; set; }

        public IList<ProductWidthViewModel> ProductWidthList { get; set; }
    }
}