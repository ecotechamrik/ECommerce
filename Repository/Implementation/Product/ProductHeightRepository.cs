using BAL.Entities;
using BAL.ViewModels.Product;
using DAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.Abstraction;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Implementation
{
    public class ProductHeightRepository : Repository<ProductHeight>, IProductHeightRepository
    {
        DatabaseContext Context
        {
            get
            {
                return db as DatabaseContext;
            }
        }
        public ProductHeightRepository(DbContext _db) : base(_db)
        {

        }        
    }
}