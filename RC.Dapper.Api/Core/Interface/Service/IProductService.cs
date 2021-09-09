using RC.Dapper.Api.Core.Request;
using RC.Dapper.Api.Core.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RC.Dapper.Api.Core.Interface
{
    public interface IProductService
    {
        Task DeleteRowAsync(int id);
        Task<ProductResponse> GetAsync(int id);
        Task UpdateAsync(ProductUpdateRequest entity);
        Task InsertAsync(ProductInsertRequest entity);
        Task<int> SaveRangeAsync(IEnumerable<ProductInsertRequest> list);
        Task<IEnumerable<ProductResponse>> GetAllAsync(int pageSize, int pageActual);
    }
}