using BAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.ModelBuilders
{
    public class WebsiteInfoBuilder
    {
        #region [ Website Information Model Builder ]
        /// <summary>
        /// Website Information Model Builder
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void WebsiteInfoModel(ModelBuilder modelBuilder)
        {
            // Setting Website Table Name and Primary Key
            modelBuilder.Entity<WebsiteInfo>(w => { w.ToTable("WebsiteInfos").HasKey(w => w.WebsiteID); w.Property(w => w.WebsiteID).ValueGeneratedOnAdd(); });

            // Setting Website Name Required, MaxLengthand and Typename to nvarchar
            modelBuilder.Entity<WebsiteInfo>().Property(w => w.WebsiteName).IsRequired(true).HasMaxLength(200).IsUnicode(true);
        }
        #endregion
    }
}
