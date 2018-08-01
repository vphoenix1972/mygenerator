using AutoMapper;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.DataAccess.Models;

namespace <%= projectNamespace %>.DataAccess.Mapping
{
    public class RefreshTokenMappingProfile : Profile
    {
        public RefreshTokenMappingProfile()
        {
            CreateMap<RefreshTokenDataModel, RefreshToken>();

            CreateMap<IRefreshToken, RefreshTokenDataModel>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}