using System.Collections.Generic;
using System.Threading.Tasks;

namespace RC.Dapper.Api.Core.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task DeleteRowAsync(int id);
        Task<TEntity> GetAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task InsertAsync(TEntity entity);
        Task<int> SaveRangeAsync(IEnumerable<TEntity> list);
        Task<IEnumerable<TEntity>> GetAllAsync(int pageSize, int pageActual);
    }
}