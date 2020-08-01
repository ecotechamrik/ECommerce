#region [ Namespace ]
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public interface IGenerateAPIResponse<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll(String apiMethod);
        Task<TEntity> GetByID(String apiMethod, int? id);
        Task<String> GetStringContent(String apiMethod);
        Task<Boolean> Save(String apiMethod, TEntity entity);
        Task<Boolean> Update(String apiMethod, TEntity entity);
        Task<Boolean> Delete(String apiMethod);
    }
}