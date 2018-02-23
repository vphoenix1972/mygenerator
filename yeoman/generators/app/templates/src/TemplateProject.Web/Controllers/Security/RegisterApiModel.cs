namespace <%= projectNamespace %>.Web.Controllers.Security
{
    public sealed class RegisterApiModel
    {
        public string Name { get; set; }

        public string EMail { get; set; }

        public string Password { get; set; }
    }
}