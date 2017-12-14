namespace TemplateProject.Utils.Factories
{
    public interface IFactory<out T>
    {
        T Create(params object[] parameters);
    }
}