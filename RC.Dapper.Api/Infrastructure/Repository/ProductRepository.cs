using RC.Dapper.Api.Core;
using RC.Dapper.Api.Core.Domains.Entitie;
using RC.Dapper.Api.Core.Interface;

namespace RC.Dapper.Api.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(ConfigurationApplication configurationApplication) : base("Product", configurationApplication)
        {

        }
    }
}
