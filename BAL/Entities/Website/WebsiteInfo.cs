using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    #region [ Multiple Website Information Details ]
    public class WebsiteInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WebsiteID { get; set; }
        
        [Column(TypeName ="nvarchar")]
        [StringLength(100)]
        public string WebsiteName { get; set; }

        [Column(TypeName = "nvarchar")]
        [StringLength(500)]
        public string URL { get; set; }
        public string CpanelUser { get; set; }
        public string CpanelPassword { get; set; }
        public string FTPUser { get; set; }
        public string FTPPassword { get; set; }
        public string DBUser { get; set; }
        public string DBPassword { get; set; }
        public string AdminUser { get; set; }
        public string AdminPassword { get; set; }
        public string DomainRenewDate { get; set; }
        public string HostingRenewDate { get; set; }
        public string HostingProviderName { get; set; }
        public string HostingProviderDesc { get; set; }
        public string HostingProviderContactNo { get; set; }
        public string WebsiteBannerTitle { get; set; }
        public string WebsiteBannerTagLine { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDesc { get; set; }
        public string ContactEmailID { get; set; }
        public string Cell { get; set; }
        public string OfficePhone { get; set; }
        public string Fax { get; set; }
        public string DevelopedBy { get; set; }
        public string Address { get; set; }
        public string AddressMap { get; set; }
    }
    #endregion
}
