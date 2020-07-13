﻿using BAL.Entities;
using BAL.ViewModels;
using System.Collections.Generic;

namespace Repository.Abstraction
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<ProductViewModel> GetProductsWithCategories();
        void DbInitialize();
    }
}