using AutoMapper;
using TemplateProject.Core.Domain;
using TemplateProject.DataAccess.Models;

namespace TemplateProject.DataAccess.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDataModel, User>();

            CreateMap<IUser, UserDataModel>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
