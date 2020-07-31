#region [ Namespace ]
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace BAL
{
    public interface IGenerateAPIResponse<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> GetAll(String apiMethod);
        public Task<TEntity> GetByID(String apiMethod, int? id);
        public Task<String> GetStringContent(String apiMethod);
        public Task<Boolean> Save(String apiMethod, TEntity entity);
        public Task<Boolean> Update(String apiMethod, TEntity entity);
        public Task<Boolean> Delete(String apiMethod);
    }
}