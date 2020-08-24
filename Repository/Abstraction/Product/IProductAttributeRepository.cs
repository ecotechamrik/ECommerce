using BAL.Entities;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IProductAttributeRepository : IRepository<ProductAttribute>
    {
        IEnumerable<ProductAttributeViewModel> GetByProductID(int? id);

        ProductAttributeViewModel GetProductAttrWithDoorName(int? id);
    }
}
