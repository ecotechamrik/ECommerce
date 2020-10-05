#region [ Namespace ]
using BAL.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public interface IProductAttributeThicknessAPIResponse : IGenerateAPIResponse<ProductAttributeThicknessViewModel>
    {
        Task<ProductAttributeThicknessViewModel> SaveModel(String apiMethod, ProductAttributeThicknessViewModel entity);
    }
}