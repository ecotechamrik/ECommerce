using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public CategoryRepository(DbContext _db) : base(_db)
        {

        }
        public IEnumerable<CategoryViewModel> GetCategoryWithSections()
        {
            var Categories = (from cat in Context.Categories
                            join sec in Context.Sections
                            on cat.SectionID equals sec.SectionID into catsections
                            
                            from sec in catsections.DefaultIfEmpty()  // Left Outer Join

                            select new CategoryViewModel
                            {
                                CategoryID = cat.CategoryID,
                                CategoryName = cat.CategoryName,
                                Description = cat.Description,
                                CategoryOrder = cat.CategoryOrder,
                                IsActive = cat.IsActive,
                                SectionID = sec.SectionID,
                                SectionName = sec.SectionName
                            }).ToList();

            return Categories;
        }
    }
}