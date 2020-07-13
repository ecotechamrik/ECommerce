using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using BAL.Entities;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        #region [ Creating Product DB Entities ]
        /// <summary>
        /// Creating Product DB Entities
        /// </summary>
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<ProductAttributes> ProductAttributes { get; set; }
        #endregion

        #region [ Create Website Info DB Entities ]
        /// <summary>
        /// Create Website Info DB Entities
        /// </summary>
        public DbSet<WebsiteInfo> WebsiteInfo { get; set; }
        #endregion

        #region [ Read Connection String from appsettings.json and Create DB Connection ]
        /// <summary>
        /// Read Connection String from appsettings.json and Create DB Connection
        /// </summary>
        //public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        //{

        //}

        // Establish DB Connection with OnConfiguring Override
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            //optionsBuilder.UseSqlServer(@"data source=208.91.198.196; initial catalog=webasqvy_ecotechdoors;user id=ecotechdoors;password=EcoTech1@3#;MultipleActiveResultSets=true");
            optionsBuilder.UseSqlServer(new SqlConnection(configuration.GetConnectionString("EcoTechCon")));
        }
        #endregion

        #region [ Setting Table Properties with Fluent API through OnModelCreating override ]
        /// <summary>
        /// Setting Table Properties with Fluent API through OnModelCreating override
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Setting Product Table Name and Primary Key
            modelBuilder.Entity<Product>(p => { p.ToTable("Products").HasKey(p => p.ProductID); p.Property(p => p.ProductID).ValueGeneratedOnAdd(); });

            // Setting Product Name Required, MaxLengthand and Typename to nvarchar
            modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired(true).HasMaxLength(500).IsUnicode(true);

            // 1:m
            //modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryID);

            // OR Alternate Way

            // Category To Products [1 - M Relationship]
            modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(c => c.Category).HasForeignKey(c => c.CategoryID).OnDelete(DeleteBehavior.SetNull);

            // Category To Products [1 - M Relationship]
            modelBuilder.Entity<SubCategory>().HasMany(c => c.Products).WithOne(c => c.SubCategory).HasForeignKey(c => c.SubCategoryID).OnDelete(DeleteBehavior.SetNull);

            // Category To SubCategories [1 - M Relationship]
            modelBuilder.Entity<Category>().HasMany(s => s.SubCategories).WithOne(c => c.Category).HasForeignKey(c => c.CategoryID).OnDelete(DeleteBehavior.SetNull);

            // Product To ProductAttributes [1 - M Relationship]
            modelBuilder.Entity<Product>().HasMany(p => p.ProductAttributes).WithOne(p => p.Product).HasForeignKey(p => p.ProductID).OnDelete(DeleteBehavior.SetNull);

        }
        #endregion
    }
}