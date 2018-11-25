using AutoMapper;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.Web.Controllers.App.Todo
{
    public sealed class TodoItemModelMappingProfile : Profile
    {
        public TodoItemModelMappingProfile()
        {
            CreateMap<TodoItem, TodoItemModel>();
            CreateMap<TodoItemModel, TodoItem>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}
