using TemplateProject.Core.Domain;

namespace TemplateProject.Core.Interfaces.DataAccess.Repositories
{
    public interface IUsersRepository
    {
        IUser Get(string nameOrEMail, string password);
    }
}