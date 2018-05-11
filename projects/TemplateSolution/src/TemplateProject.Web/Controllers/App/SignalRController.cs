using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TemplateProject.Web.Hubs;

namespace TemplateProject.Web.Controllers.App
{
    public sealed class SignalrController : AppControllerBase
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public SignalrController(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromBody] Message message)
        {
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.User, message.Text);

            return Ok();
        }

        public class Message
        {
            public string User { get; set; }

            public string Text { get; set; }
        }
    }
}