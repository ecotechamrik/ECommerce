using BAL.Entities;
using BAL.ViewModels.Product;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IProductThicknessRepository : IRepository<ProductThickness>
    {
        IEnumerable<ProductThicknessViewModel> GetWithAttributeThicknessID(int? DoorTypeID);
    }
}
