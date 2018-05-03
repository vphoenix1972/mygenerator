namespace TemplateProject.Web.Configuration
{
    public interface IWebConfiguration
    {
        string ServerUrls { get; }

        string DbConnectionString { get; }
    }
}