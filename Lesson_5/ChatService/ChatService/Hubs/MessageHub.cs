using ChatService.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatService.Hubs
{
    public class MessageHub : Hub
    {
        public Task MessageAll(Message message)
        {
            //Clients.Client("HUB_ID").SendAsync("MessageReceived", message);
            return Clients.All.SendAsync("MessageReceived", message);
        }
    }
}
