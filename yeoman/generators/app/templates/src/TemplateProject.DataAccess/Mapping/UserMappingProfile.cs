using AutoMapper;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.DataAccess.Models;

namespace <%= projectNamespace %>.DataAccess.Mapping
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