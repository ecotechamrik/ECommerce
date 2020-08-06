using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductSizeAndPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductSizeAndPricesID { get; set; }
        public string ProductCode { get; set; }
        public int? ProductAttributeID { get; set; }
        public int? ProductSizeID { get; set; }
        public int? CurrencyID { get; set; }
        public DateTime PriceDate { get; set; }
        public DateTime InvDate { get; set; }        
        public double RetailPriceDisc { get; set; }
        public double PriceVoid { get; set; }
        public double Markup { get; set; }
        public double SellingPrice { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }


        /* -- Supplier Details -- */
        public int? SupplierID { get; set; }
        public double InboundCost { get; set; }
        public double LandedCost { get; set; }


        /* -- Navigation Properties -- */
        public ProductAttribute ProductAttribute { get; set; }
        public ProductSize ProductSize { get; set; }
        public Currency Currency { get; set; }
        public Supplier Supplier { get; set; }        
    }
}