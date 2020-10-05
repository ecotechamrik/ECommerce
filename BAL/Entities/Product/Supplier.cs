using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class Supplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public bool IsActive { get; set; }

        /* -- Navigation Properties -- */
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public ICollection<ProductSupplier> ProductSuppliers { get; set; }
    }
}