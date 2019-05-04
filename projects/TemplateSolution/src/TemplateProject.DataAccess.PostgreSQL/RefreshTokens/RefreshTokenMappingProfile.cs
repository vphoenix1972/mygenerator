using AutoMapper;
using TemplateProject.Core.Domain;

namespace TemplateProject.DataAccess.PostgreSQL.RefreshTokens
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
