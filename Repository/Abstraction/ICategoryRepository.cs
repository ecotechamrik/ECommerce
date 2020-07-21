using BAL.Entities;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<CategoryViewModel> GetCategoryWithSections();
    }
}
