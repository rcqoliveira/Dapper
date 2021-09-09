using AutoMapper;
using RC.Dapper.Api.Core.Domains.Entitie;
using RC.Dapper.Api.Core.Request;
using RC.Dapper.Api.Core.Response;

namespace RC.Dapper.Api.Core.AutoMapper
{
    public class ProductionProfile : Profile
    {
        public ProductionProfile()
        {
            CreateMap<Product, ProductInsertRequest>().ReverseMap();
            CreateMap<Product, ProductUpdateRequest>().ReverseMap();
            CreateMap<Product, ProductResponse>().ReverseMap();
        }
    }
}
