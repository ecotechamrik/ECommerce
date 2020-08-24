using BAL.Entities;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IProductSizeAndPriceRepository : IRepository<ProductSizeAndPrice>
    {
        IEnumerable<ProductSizeAndPriceViewModel> GetByProductAttributeID(int? ProductAttributeID);
        IEnumerable<ProductSizeAndPriceViewModel> GetByProductWidthID(int ProductAttributeThicknessID, int ProductWidthID);
    }
}
