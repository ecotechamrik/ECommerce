using BAL.Entities;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IProductSupplierRepository : IRepository<ProductSupplier>
    {
        IEnumerable<ProductSupplierViewModel> GetByProductSizeAndPriceID(int ProductSizeAndPriceID);
    }
}
