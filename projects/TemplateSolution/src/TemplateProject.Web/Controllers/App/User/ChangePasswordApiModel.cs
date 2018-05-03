namespace TemplateProject.Web.Controllers.App.User
{
    public sealed class ChangePasswordApiModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}