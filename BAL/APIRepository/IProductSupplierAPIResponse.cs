#region [ Namespace ]
using BAL.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public interface IProductSupplierAPIResponse : IGenerateAPIResponse<ProductSupplierViewModel>
    {
        Task<ProductSupplierViewModel> SaveModel(String apiMethod, ProductSupplierViewModel entity);
    }
}