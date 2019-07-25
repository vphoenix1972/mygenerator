using AutoMapper;
using TemplateProject.Core.Domain;

namespace TemplateProject.Web.Controllers.App.Todo
{
    public sealed class TodoItemsApiMappingProfile : Profile
    {
        public TodoItemsApiMappingProfile()
        {
            CreateMap<TodoItem, TodoItemApiDto>();
            CreateMap<TodoItemApiDto, TodoItem>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
