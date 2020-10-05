#region [ Namespace ]
using BAL.ViewModels.Product;
using System;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public interface IProductImagesAPIResponse: IGenerateAPIResponse<ProductImageViewModel>
    {
        Task<Boolean> UpdateOrder(string apiMethod, int id, int orderno);
    }
}