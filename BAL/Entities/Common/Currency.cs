using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CurrencyID { get; set; }

        [Required]
        public string CurrencyName { get; set; }

        public bool IsDefault { get; set; } = false;
    }
}