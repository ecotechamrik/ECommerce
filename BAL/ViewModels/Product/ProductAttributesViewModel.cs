using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.ViewModels.Product
{
    public class ProductAttributesViewModel
    {
        public int ProductAttributeID { get; set; }
        public int? ProductID { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public int? DoorTypeID { get; set; }
        public bool IsActive { get; set; }        
    }
}
