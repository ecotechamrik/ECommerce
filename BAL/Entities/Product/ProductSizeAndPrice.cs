using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductSizeAndPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductSizeAndPriceID { get; set; }

        [MaxLength(500)]
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int? ProductAttributeThicknessID { get; set; }
        public int? ProductHeightID { get; set; }
        public int? ProductWidthID { get; set; }
        public DateTime? PriceVoidDate { get; set; }
        public DateTime? PriceDate { get; set; }
        public DateTime? InvDate { get; set; }
        public double? RetailPriceDisc { get; set; }
        public double? RetailPrice { get; set; }
        public double? PriceVoid { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

        /* -- Addition Properties -- */
        public double? PriorityNumber { get; set; }
        public string InventoryNumber { get; set; }
        public string Notes { get; set; }
        public double? GroupNumber { get; set; }
        public string BuildingCode { get; set; }
        public string LocationCode { get; set; }
        public int? InventoryLevel { get; set; }
        public string LeadTime { get; set; }
        public int? BestQuantityNo { get; set; }
        public int? OrderNowNo { get; set; }
        public string RetailBin { get; set; }
        public string WholeSaleBin { get; set; }
        public int? IndexNumber { get; set; }

        public double? PracticalMarkup { get; set; }
        public double? PracticalCost { get; set; }

        public double? Markup { get; set; }
        public double? SellingPrice { get; set; }

        public double? RetailMarkupDisc { get; set; }
        public double? LivePriceDisc { get; set; }

        /* -- Navigation Properties -- */
        public ProductAttributeThickness ProductAttributeThickness { get; set; }
        public ProductHeight ProductHeight { get; set; }
        public ProductWidth ProductWidth { get; set; }
        public ICollection<ProductSupplier> ProductSuppliers { get; set; }
    }
}