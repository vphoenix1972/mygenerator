using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TemplateProject.Core.Domain;

namespace TemplateProject.DataAccess.MongoDB.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserDataModel, User>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id.ToString()))
                .ForMember(d => d.Roles, o => o.MapFrom((s, d) =>
                    {
                        if (s.Roles == null)
                            return new List<IUserRole>();

                        return s.Roles.Select(x => (IUserRole) new UserRole() { Name = x }).ToList();
                    }));

            CreateMap<IUser, UserDataModel>()
                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Roles, o => o.MapFrom(s => s.Roles.Select(x => x.Name).ToList()));
        }
    }
}
