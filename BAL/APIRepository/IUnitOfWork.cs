using BAL.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL
{
    public interface IUnitOfWork
    {
        IGenerateAPIResponse<CategoryViewModel> CategoryViewRepo { get; }
        IGenerateAPIResponse<SubCategoryViewModel> SubCategoryViewRepo { get; }
        IGenerateAPIResponse<ProductViewModel> ProductViewRepo { get; }
        IGenerateAPIResponse<DoorTypeViewModel> DoorTypeViewRepo { get; }
        IGenerateAPIResponse<ProductDesignViewModel> ProductDesignViewRepo { get; }
        IGenerateAPIResponse<ProductGradeViewModel> ProductGradeViewRepo { get; }
        
    }
}
