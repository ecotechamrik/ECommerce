using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductGrade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductGradeID { get; set; }
        public string ProductGradeName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
