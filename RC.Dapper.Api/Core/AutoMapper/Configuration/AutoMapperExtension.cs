using RC.Dapper.Api.Core.AutoMapper.Configuration;

namespace RC.Dapper.Api.Core.AutoMapper
{
    public static class AutoMapperExtension
    {
        public static TDestination MapTo<TSource, TDestination>(this TSource source)
        {
            return AutoMapperConfiguration.Mapper.Map<TSource, TDestination>(source);
        }

        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            return AutoMapperConfiguration.Mapper.Map(source, destination);
        }
    }
}
