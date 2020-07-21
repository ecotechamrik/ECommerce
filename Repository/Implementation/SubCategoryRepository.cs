using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public SubCategoryRepository(DbContext _db) : base(_db)
        {

        }
        public IEnumerable<SubCategoryViewModel> GetSubCategoryWithCategories()
        {
            var SubCategories = (from subcat in Context.SubCategories
                            join cat in Context.Categories
                            on subcat.CategoryID equals cat.CategoryID into subcategories
                            
                            from cat in subcategories.DefaultIfEmpty()  // Left Outer Join

                            select new SubCategoryViewModel
                            {
                                SubCategoryID = subcat.SubCategoryID,
                                SubCategoryName = subcat.SubCategoryName,
                                Description = subcat.Description,
                                IsActive = subcat.IsActive,
                                CategoryID = cat.CategoryID,
                                CategoryName = cat.CategoryName
                            }).ToList();

            return SubCategories;
        }
    }
}