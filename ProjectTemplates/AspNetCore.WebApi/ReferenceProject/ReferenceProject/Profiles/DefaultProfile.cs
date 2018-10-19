using AutoMapper;

namespace ReferenceProject.Profiles
{
    public class DefaultProfile: Profile
    {
        public DefaultProfile()
        {
            CreateMap<Model.Product, Dto.Product>();
            // For copy creation
            CreateMap<Model.Product, Model.Product>();
        }
    }
}
