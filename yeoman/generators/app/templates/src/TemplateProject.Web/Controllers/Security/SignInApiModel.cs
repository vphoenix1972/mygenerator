namespace <%= projectNamespace %>.Web.Controllers.Security
{
    public sealed class SignInApiModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}