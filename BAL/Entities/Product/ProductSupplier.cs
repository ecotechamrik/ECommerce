using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BAL.Entities
{
    public class ProductSupplier
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int ProductSupplierID { get; set; }
        public int? ProductSizeAndPriceID { get; set; }
        public int? SupplierID { get; set; }
        
        public double InboundCost { get; set; } = 0;
        public double TransportationCost { get; set; } = 0;
        public double LandedCost { get; set; } = 0;
        public bool IsOption { get; set; }
        public bool IsLive { get; set; }

        public Supplier Supplier { get; set; }
        public ProductSizeAndPrice ProductSizeAndPrice { get; set; }
    }
}