using BAL.Entities;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IProductAttributeRepository : IRepository<ProductAttribute>
    {
        IEnumerable<ProductAttributeViewModel> GetAttributesByID(int? id);
        IEnumerable<ProductAttributeViewModel> GetAttributesByProductID(int? id);
        void UpdateProductAttribute(ProductAttribute model);
    }
}
