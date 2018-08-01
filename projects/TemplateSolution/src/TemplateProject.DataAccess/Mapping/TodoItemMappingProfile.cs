using AutoMapper;
using TemplateProject.Core.Domain;
using TemplateProject.DataAccess.Models;

namespace TemplateProject.DataAccess.Mapping
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
