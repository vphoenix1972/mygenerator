using AutoMapper;
using <%= projectNamespace %>.Core.Domain;

namespace <%= projectNamespace %>.DataAccess.PostgreSQL.TodoItems
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
