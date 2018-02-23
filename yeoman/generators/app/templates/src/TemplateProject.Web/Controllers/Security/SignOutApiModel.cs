namespace <%= projectNamespace %>.Web.Controllers.Security
{
    public sealed class SignOutApiModel
    {
        public string RefreshToken { get; set; }
    }
}