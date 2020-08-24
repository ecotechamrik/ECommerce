using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductSizeAndPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductSizeAndPriceID { get; set; }
        public string ProductCode { get; set; }
        public int? ProductAttributeThicknessID { get; set; }
        public int? ProductHeightID { get; set; }
        public int? ProductWidthID { get; set; }       
        public DateTime PriceDate { get; set; }
        public DateTime InvDate { get; set; }        
        public double RetailPriceDisc { get; set; }
        public double PriceVoid { get; set; }
        public double Markup { get; set; }
        public double SellingPrice { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }


        /* -- Supplier Details -- */
        public int? SupplierID { get; set; }
        public double InboundCost { get; set; }        
        public double TransportationCost { get; set; }
        public double LandedCost { get; set; }


        /* -- Navigation Properties -- */
        public ProductAttributeThickness ProductAttributeThickness { get; set; }
        public ProductHeight ProductHeight { get; set; }
        public ProductWidth ProductWidth { get; set; }        
        public Supplier Supplier { get; set; }
    }
}