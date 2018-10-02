using AutoMapper;
using System;
using System.Web.Http;

namespace ReferenceProject
{
    public class AutoMapperConfig
    {
        protected internal static IMapper Mapper;

        static Func<Type, object> GetResolver(HttpConfiguration config) => type => config.DependencyResolver.GetService(type);

        public static IMapper Configure(HttpConfiguration config)
        {
            Action<IMapperConfigurationExpression> mapperConfigurationExp = cfg =>
            {
                cfg.ConstructServicesUsing(GetResolver(config));

                // TODO: Create mappings here
            };

            var mapperConfiguration = new MapperConfiguration(mapperConfigurationExp);
            Mapper = mapperConfiguration.CreateMapper();

            return Mapper;
        }
    }
}