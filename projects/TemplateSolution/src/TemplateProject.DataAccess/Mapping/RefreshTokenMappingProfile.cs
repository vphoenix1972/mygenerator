using AutoMapper;
using TemplateProject.Core.Domain;
using TemplateProject.DataAccess.Models;

namespace TemplateProject.DataAccess.Mapping
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
