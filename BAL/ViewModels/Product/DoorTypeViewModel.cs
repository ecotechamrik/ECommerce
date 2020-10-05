using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class DoorTypeViewModel
    {
        public int DoorTypeID { get; set; }

        [Required(ErrorMessage ="Please Enter Door Type Name")]
        [DisplayName("Door Type Name")]
        public string DoorTypeName { get; set; }
        public string DoorTypeCode { get; set; }
    }
}
