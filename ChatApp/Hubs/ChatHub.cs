using Microsoft.AspNetCore.SignalR;
using ChatApp.Data;
using ChatApp.Models;

namespace ChatApp.Hubs;

public class ChatHub : Hub
{
    private readonly ChatContext _context;

    public ChatHub(ChatContext context)
    {
        _context = context;
    }

    public async Task SendMessage(string user, string message)
    {
        var newMessage = new Message
        {
            User = user,
            Content = message,
            Timestamp = DateTime.UtcNow
        };

        _context.Messages.Add(newMessage);
        await _context.SaveChangesAsync();

        await Clients.All.SendAsync("ReceiveMessage", user, message, newMessage.Timestamp);
    }
}