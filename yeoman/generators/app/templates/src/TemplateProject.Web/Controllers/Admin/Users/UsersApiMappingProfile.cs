using AutoMapper;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.Admin.Users
{
    public sealed class UsersApiMappingProfile : Profile
    {
        public UsersApiMappingProfile()
        {
            CreateMap<IUser, UserApiDto>();
        }
    }
}