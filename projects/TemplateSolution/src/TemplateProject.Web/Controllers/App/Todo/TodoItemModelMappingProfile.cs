using AutoMapper;
using TemplateProject.Core.Domain;

namespace TemplateProject.Web.Controllers.App.Todo
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
