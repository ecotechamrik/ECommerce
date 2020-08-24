#region [ Namespace ]
using BAL.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public interface IProductAttributeAPIResponse : IGenerateAPIResponse<ProductAttributeViewModel>
    {
        Task<ProductAttributeViewModel> GetProductAttrWithDoorName(string apiMethod, int id);
        Task<ProductAttributeViewModel> SaveModel(String apiMethod, ProductAttributeViewModel entity);
    }
}