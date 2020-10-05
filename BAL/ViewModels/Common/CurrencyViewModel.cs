using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Common
{
    public class CurrencyViewModel
    {
        public int CurrencyID { get; set; }

        [Required(ErrorMessage = "Please enter Currency Name")]
        [DisplayName("Currency Name")]
        public string CurrencyName { get; set; }


        [DisplayName("Is Default Currency?")]
        public bool IsDefault { get; set; } = false;
    }
}
