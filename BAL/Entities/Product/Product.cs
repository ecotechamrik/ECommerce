using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class Product
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }

        //[Required]
        //[Column(TypeName = "nvarchar")]
        //[StringLength(200)]
        public string ProductName { get; set; }

        //[Required]
        //[Column(TypeName = "nvarchar")]
        //[StringLength(200)]
        public string ProductCode { get; set; }

        //[Required]
        //[Column(TypeName = "nvarchar(500)")]
        public string ProductDesc { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string PriorityNumber { get; set; }

        //[Column(TypeName = "nvarchar(500)")]
        public string Notes { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string GroupNumber { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string BuildingCode { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string LocationCode { get; set; }

        public int? SupplierID { get; set; }

        //[Column(TypeName = "nvarchar(100)")]
        public string SupplierModeCode { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string LeadTime { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string RetailBin { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string WholeSaleBin { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string IndexNumber { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string PracticalMarkup { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string PracticalCost { get; set; }

        //[Column(TypeName = "nvarchar(200)")]
        public string RetailMarkupDisc { get; set; }

        //[Column(TypeName = "nvarchar(100)")]
        public string RetailMarkup { get; set; }

        //[Column(TypeName = "nvarchar(200)")]
        public string LivePriceDisc { get; set; }

        //[Column(TypeName = "nvarchar(50)")]
        public string LivePrice { get; set; }

        //[Column(TypeName = "nvarchar(10)")]
        public string Width { get; set; }

        public int? CategoryID { get; set; }

        public int? SubCategoryID { get; set; }

        //[Column(TypeName = "nvarchar(20)")]
        public string IPAddress { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }

        // Navigation Property
        //[ForeignKey("CategoryID")] //-- CategoryID
        public Category Category { get; set; }

        //[ForeignKey("SubCategoryID")] //-- SubCategoryID        
        public SubCategory SubCategory { get; set; }
        public ICollection<ProductAttributes> ProductAttributes { get; set; }
    }
}
