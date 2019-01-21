using AutoMapper;

namespace ReferenceProject.Profiles
{
    /// <summary>
    /// Default profile for AutoMapper
    /// </summary>
    /// <remarks>See: https://github.com/drwatson1/AspNet-Core-REST-Service/wiki#automapper</remarks>
    public class DefaultProfile: Profile
    {
        public DefaultProfile()
        {
            CreateMap<Model.Product, Dto.Product>();
            CreateMap<Dto.UpdateProduct, Model.Product>();
            // For copy creation
            CreateMap<Model.Product, Model.Product>();
        }
    }
}
