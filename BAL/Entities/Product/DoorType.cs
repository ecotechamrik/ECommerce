using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class DoorType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DoorTypeID { get; set; }
        public string DoorTypeName { get; set; }
        public ICollection<ProductAttributes> ProductAttributes { get; set; }
    }
}
