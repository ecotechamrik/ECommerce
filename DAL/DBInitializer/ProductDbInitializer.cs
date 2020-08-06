using BAL.Entities;
using System;
using System.Linq;

namespace DAL.DBInitializer
{
    public class ProductDbInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            //context.Database.EnsureCreated();

            // Look for any Categories.
            if (context.Categories.Any())
            {
                return;   // DB has been seeded
            }

            // Add Main Categories
            var categories = new Category[]
            {
                new Category { CategoryName = "Interior Doors", Description = "Interior Doors", IsActive = true },
                new Category { CategoryName = "Exterior Doors", Description = "Exterior Doors", IsActive = true },
                new Category { CategoryName = "Others", Description = "Others", IsActive = true }
            };

            context.Categories.AddRange(categories);
            context.SaveChanges();

            // Get Interior, Exterior, Other Category IDs to use further
            var InteriorCategoryID = categories.Single(c => c.CategoryName == "Interior Doors").CategoryID;
            var ExteriorCategoryID = categories.Single(c => c.CategoryName == "Exterior Doors").CategoryID;
            var OtherCategoryID = categories.Single(c => c.CategoryName == "Others").CategoryID;

            // Add Sub Categories
            var subcategories = new SubCategory[]
            {
                // Interior Sub Categories
                new SubCategory { SubCategoryName = "Interior Traditional Paint Grade Doors", Description = "Interior Traditional Paint Grade Doors",
                                  CategoryID = InteriorCategoryID,  IsActive = true },

                new SubCategory { SubCategoryName = "Interior Traditional Stain Grade Doors", Description = "Interior Traditional Stain Grade Doors",
                                  CategoryID = InteriorCategoryID,  IsActive = true },

                new SubCategory { SubCategoryName = "Interior Contemporary Doors", Description = "Interior Contemporary Doors",
                                  CategoryID = InteriorCategoryID,  IsActive = true },

                // Exterior Sub Categories
                new SubCategory { SubCategoryName = "Exterior Traditional Paint Grade Doors", Description = "Exterior Traditional Paint Grade Doors",
                                  CategoryID = ExteriorCategoryID,  IsActive = true },

                new SubCategory { SubCategoryName = "Exterior Traditional Stain Grade Doors", Description = "Exterior Traditional Stain Grade Doors",
                                  CategoryID = ExteriorCategoryID,  IsActive = true },

                new SubCategory { SubCategoryName = "Exterior Contemporary Doors", Description = "Exterior Contemporary Doors",
                                  CategoryID = ExteriorCategoryID,  IsActive = true },

                new SubCategory { SubCategoryName = "Exterior French Doors", Description = "Exterior French Doors",
                                  CategoryID = ExteriorCategoryID,  IsActive = true },

                // Other Sub Categories
                new SubCategory { SubCategoryName = "Hardware", Description = "Hardware",
                                  CategoryID = OtherCategoryID,  IsActive = true },

                new SubCategory { SubCategoryName = "Installation Services", Description = "Installation Services",
                                  CategoryID = OtherCategoryID,  IsActive = true },

                new SubCategory { SubCategoryName = "Finishing", Description = "Finishing",
                                  CategoryID = OtherCategoryID,  IsActive = true }
            };

            context.SubCategories.AddRange(subcategories);
            context.SaveChanges();

            // Get Interior, Exterior, Other Category IDs to use further
            var InteriorSubCategoryID = subcategories.Single(s => s.SubCategoryName == "Interior Traditional Paint Grade Doors").SubCategoryID;

            var products = new Product[]
            {
                new Product { ProductName = "Eyremount", 
                              ProductDesc = "This is a beautiful Mahogany Unit built from solid mahogany lumber straight from South America. The 4 panel arch top design includes a sashed all around full decorative lite transom.",
                              CategoryID = InteriorCategoryID, SubCategoryID = InteriorSubCategoryID, CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString()) },

                new Product { ProductName = "Altamont", 
                              ProductDesc = "This is a beautiful mahogany unit. A traditional 2 panel design, a perfect fit for any style of houses. Paired with a slanted sashed sidelite with 1 panel bottom.",
                              CategoryID = InteriorCategoryID, SubCategoryID = InteriorSubCategoryID, CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString()) },

                new Product { ProductName = "Shaunghnessy", 
                              ProductDesc = "The 5 panel design with an arch top",
                              CategoryID = InteriorCategoryID, SubCategoryID = InteriorSubCategoryID, CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString()) },

                new Product { ProductName = "Chateau", 
                              ProductDesc = "A mahogany unit with a special designed 2 panel. This unit includes a all around sashed transom unit.",
                              CategoryID = InteriorCategoryID, SubCategoryID = InteriorSubCategoryID, CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString()) },

                new Product { ProductName = "Bordeaux", 
                              ProductDesc = "A simple design grooved and arched top",
                              CategoryID = InteriorCategoryID, SubCategoryID = InteriorSubCategoryID, CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString()) },
            };

            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}
