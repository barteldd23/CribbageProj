using Microsoft.AspNetCore.SignalR;

namespace Cribbage.API.Hubs
{
    public class CribbageHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Do BL Stuff - Game Logic
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
