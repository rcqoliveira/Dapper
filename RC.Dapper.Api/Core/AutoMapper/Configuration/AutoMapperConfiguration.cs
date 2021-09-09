using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace RC.Dapper.Api.Core.AutoMapper.Configuration
{
    public class AutoMapperConfiguration
    {
        private static readonly object ConcurrentLock = new object();
        public static IMapper Mapper { get; private set; }
        public static MapperConfiguration MapperConfiguration { get; private set; }
        private static bool _initialized;

        public static void Initialize()
        {
            lock (ConcurrentLock)
                if (!_initialized)
                {
                    MapperConfiguration = new MapperConfiguration(cfg =>
                    {
                        var profiles = Assembly.GetExecutingAssembly()
                                               .GetExportedTypes()
                                               .Where(x => x.IsClass && typeof(Profile).IsAssignableFrom(x));

                        foreach (var profile in profiles)
                        {
                            cfg.AddProfile((Profile)Activator.CreateInstance(profile));
                        }

                    });

                    _initialized = true;
                }

            Mapper = MapperConfiguration.CreateMapper();
        }
    }
}
