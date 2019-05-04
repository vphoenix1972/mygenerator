using AutoMapper;
using TemplateProject.Core.Domain;

namespace TemplateProject.DataAccess.SQLServer.Users
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
