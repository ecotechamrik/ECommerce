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
        Task<ProductSizeAndPriceViewModel> SaveModel(String apiMethod, ProductSizeAndPriceViewModel entity);
        Task<Boolean> UpdatePriceVoid(string apiMethod, int id, double pricevoid);
    }
}