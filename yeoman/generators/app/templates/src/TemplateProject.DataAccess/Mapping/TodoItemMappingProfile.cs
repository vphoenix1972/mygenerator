using AutoMapper;
using <%= projectNamespace %>.Core.Domain;
using <%= projectNamespace %>.DataAccess.Models;

namespace <%= projectNamespace %>.DataAccess.Mapping
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