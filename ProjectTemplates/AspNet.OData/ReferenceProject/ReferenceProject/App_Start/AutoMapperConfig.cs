using AutoMapper;
using System;
using System.Web.Http;

namespace ReferenceProject
{
    public class AutoMapperConfig
    {
        protected internal static IMapper Mapper;

        public static void Configure()
        {
            Action<IMapperConfigurationExpression> mapperConfigurationExp = cfg =>
            {
                // TODO: Create mappings here
            };

            var mapperConfiguration = new MapperConfiguration(mapperConfigurationExp);
            Mapper = mapperConfiguration.CreateMapper();
        }
    }
}