using AutoMapper;
using TemplateProject.Core.Domain;

namespace TemplateProject.Web.Controllers.Admin.Users
{
    public sealed class UsersApiMappingProfile : Profile
    {
        public UsersApiMappingProfile()
        {
            CreateMap<IUser, UserApiDto>();
        }
    }
}
