using Microsoft.AspNetCore.SignalR;
namespace BlazorApp1.Models
{
    public class NotificationHub : Hub
    {
        public async Task NotifyHomePageVisit(string userEmail)
        {
            // This method will send a notification to all connected clients
            await Clients.All.SendAsync("ReceiveNotification", $"{userEmail} is visiting the homepage.");
        }
    }

}
