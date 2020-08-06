using BAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.ModelBuilders
{
    public class ProductBuilder
    {
        #region [ Product Model Builder ]
        /// <summary>
        /// Product Model Builder
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ProductModel(ModelBuilder modelBuilder)
        {
            // Setting Product Table Name and Primary Key
            modelBuilder.Entity<Product>(p => { p.ToTable("Products").HasKey(p => p.ProductID); p.Property(p => p.ProductID).ValueGeneratedOnAdd(); });

            // Setting Product Name Required, MaxLengthand and Typename to nvarchar
            modelBuilder.Entity<Product>().Property(p => p.ProductName).IsRequired(true).HasMaxLength(500).IsUnicode(true);

            // 1:m
            //modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryID);

            // OR Alternate Way

            // Section To Categories [1 - M Relationship with Section]
            modelBuilder.Entity<Section>().HasMany(s => s.Categoriess).WithOne(s => s.Section).HasForeignKey(s => s.SectionID).OnDelete(DeleteBehavior.SetNull);

            // Category To Products [1 - M Relationship with Category]
            modelBuilder.Entity<Category>().HasMany(c => c.Products).WithOne(c => c.Category).HasForeignKey(c => c.CategoryID).OnDelete(DeleteBehavior.SetNull);

            // SubCategory To Products [1 - M Relationship with SubCategory]
            modelBuilder.Entity<SubCategory>().HasMany(c => c.Products).WithOne(c => c.SubCategory).HasForeignKey(c => c.SubCategoryID).OnDelete(DeleteBehavior.SetNull);

            // Category To SubCategories [1 - M Relationship with Category]
            modelBuilder.Entity<Category>().HasMany(s => s.SubCategories).WithOne(c => c.Category).HasForeignKey(c => c.CategoryID).OnDelete(DeleteBehavior.SetNull);

            // SubCategory To SubCategoryGallery [1 - M Relationship with SubCategory]
            modelBuilder.Entity<SubCategory>().HasMany(s => s.SubCatGallery).WithOne(s => s.SubCategory).HasForeignKey(s => s.SubCategoryID).OnDelete(DeleteBehavior.SetNull);

            // Product To ProductImages [1 - M Relationship with Product]
            modelBuilder.Entity<Product>().HasMany(p => p.ProductImages).WithOne(p => p.Product).HasForeignKey(p => p.ProductID).OnDelete(DeleteBehavior.SetNull);

            // DoorType To ProductAttributes [1 - M Relationship with DoorType]
            modelBuilder.Entity<DoorType>().HasMany(c => c.ProductAttributes).WithOne(c => c.DoorType).HasForeignKey(c => c.DoorTypeID).OnDelete(DeleteBehavior.SetNull);

            // ProductDesign To Products [1 - M Relationship with ProductDesign]
            modelBuilder.Entity<ProductDesign>().HasMany(c => c.Products).WithOne(c => c.ProductDesign).HasForeignKey(c => c.ProductDesignID).OnDelete(DeleteBehavior.SetNull);

            // ProductGrade To Products [1 - M Relationship with ProductGrade]
            modelBuilder.Entity<ProductGrade>().HasMany(c => c.Products).WithOne(c => c.ProductGrade).HasForeignKey(c => c.ProductGradeID).OnDelete(DeleteBehavior.SetNull);

            // Product To ProductAttributes [1 - M Relationship with Product]
            modelBuilder.Entity<Product>().HasMany(p => p.ProductAttributes).WithOne(p => p.Product).HasForeignKey(p => p.ProductID).OnDelete(DeleteBehavior.SetNull);

            // ProductAttributes To ProductSizeAndPrices [1 - M Relationship with ProductAttributes]
            modelBuilder.Entity<ProductAttribute>().HasMany(p => p.ProductSizeAndPrices).WithOne(p => p.ProductAttribute).HasForeignKey(p => p.ProductAttributeID).OnDelete(DeleteBehavior.SetNull);

            // ProductSize To ProductSizeAndPrices [1 - M Relationship with Product Sizes & Prices]
            modelBuilder.Entity<ProductSize>().HasMany(p => p.ProductSizeAndPrices).WithOne(p => p.ProductSize).HasForeignKey(p => p.ProductSizeID).OnDelete(DeleteBehavior.SetNull);

            // Supplier To ProductSizeAndPrices [1 - M Relationship with Product Sizes & Supplier]
            modelBuilder.Entity<Supplier>().HasMany(p => p.ProductSizeAndPrices).WithOne(p => p.Supplier).HasForeignKey(p => p.SupplierID).OnDelete(DeleteBehavior.SetNull);
        }
        #endregion
    }
}
