using ChatService.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    internal class MessageClient
    {
        private HubConnection _hubConnection;
        private int _id;

        public MessageClient(int id)
        {
            _id = id;
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(new Uri("http://localhost:5131/hub/messages"))
                .WithAutomaticReconnect()
                .Build();
            _hubConnection.StartAsync().Wait();
            var connection = _hubConnection.ConnectionId;
            _hubConnection.On<Message>("MessageReceived", OnMessageReceivedAsync);
        }

        private void OnMessageReceivedAsync(Message message)
        {
            Console.WriteLine($"#{message.FromId} -> {message.ToId} >>> {message.Text}");
        }

        public Task SendMessageAsync(int toId, string text)
        {
            return _hubConnection.InvokeAsync("MessageAll", new Message
            {
                FromId = _id,
                ToId = toId,
                Text = text
            });
        }

    }
}
