using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace TemplateProject.Web.Hubs
{
    public class ChatHub : Hub
    {
        public async Task<string> SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);

            return "Success!";
        }
    }
}