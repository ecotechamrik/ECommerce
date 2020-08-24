using BAL.Entities;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IProductHeightRepository : IRepository<ProductHeight>
    {
        IEnumerable<ProductHeightViewModel> GetProductHeightNotAdded(int? ProductAttrID, int? ProductSizeAndPriceID);
    }
}
