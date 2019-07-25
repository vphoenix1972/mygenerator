using AutoMapper;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.App.Todo
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
