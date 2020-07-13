using BAL.Entities;
using Repository.Abstraction;

namespace Repository
{
    public interface IUnitOfWork
    {
        IRepository<Category> CategoryRepo { get; }
        IProductRepository ProductRepo { get; }
        int SaveChanges();
    }
}
