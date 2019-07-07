using AutoMapper;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.DataAccess.SQLServer.TodoItems
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<TodoItemDataModel, TodoItem>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id.ToString()));

            CreateMap<ITodoItem, TodoItemDataModel>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }

}
