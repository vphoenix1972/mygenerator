using AutoMapper;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.DataAccess.SQLite.Users
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
