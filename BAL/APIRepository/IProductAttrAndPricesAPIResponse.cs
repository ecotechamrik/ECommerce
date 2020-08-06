#region [ Namespace ]
using BAL.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public interface IProductSizeAndPriceAPIResponse : IGenerateAPIResponse<ProductSizeAndPriceViewModel>
    {
        Task<IEnumerable<ProductSizeAndPriceViewModel>> GetByProductID(string apiMethod, int? pid);
    }
}