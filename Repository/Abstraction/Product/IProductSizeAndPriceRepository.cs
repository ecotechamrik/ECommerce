using BAL.Entities;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IProductSizeAndPriceRepository : IRepository<ProductSizeAndPrice>
    {
        void UpdatePriceVoid(int ProductSizeAndPriceID, double OrderNo);
        IEnumerable<ProductSizeAndPriceViewModel> GetByProductWidthID(int ProductAttributeThicknessID, int ProductWidthID);
        IEnumerable<ProductSizeAndPriceViewModel> ProductAttributeDetails(int ProductAttributeID);
    }
}
