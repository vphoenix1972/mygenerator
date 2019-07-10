using AutoMapper;
using <%= csprojName %>.Core.Domain;

namespace <%= csprojName %>.DataAccess.MongoDB.TodoItems
{
    public class TodoItemMappingProfile : Profile
    {
        public TodoItemMappingProfile()
        {
            CreateMap<TodoItemDataModel, TodoItem>();

            CreateMap<ITodoItem, TodoItemDataModel>()
                .ForMember(d => d.Id, o => o.Ignore());
        }
    }
}