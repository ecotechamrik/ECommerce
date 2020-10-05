using BAL.Entities;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<ProductViewModel> GetProductsWithCategories();
        IEnumerable<ProductViewModel> SearchProducts(string search);
        void DbInitialize();
    }
}
