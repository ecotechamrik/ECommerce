using BAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.ModelBuilders
{
    public class UserInfoBuilder
    {
        #region [ User Information Model Builder ]
        /// <summary>
        /// User Information Model Builder
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void UserInfoModel(ModelBuilder modelBuilder)
        {
            // Setting User Table Name and Primary Key
            modelBuilder.Entity<User>(u => { u.ToTable("Users").HasKey(u => u.UserID); u.Property(u => u.UserID).ValueGeneratedOnAdd(); });

            // Setting UserName Required, MaxLengthand and Typename to nvarchar
            modelBuilder.Entity<User>().Property(u => u.UserName).IsRequired(true).HasMaxLength(100).IsUnicode(true);
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired(true).HasMaxLength(100).IsUnicode(true);
        }
        #endregion
    }
}