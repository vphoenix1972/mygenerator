using AutoMapper;
using TemplateProject.Core.Domain;

namespace TemplateProject.DataAccess.PostgreSQL.TodoItems
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
