using Microsoft.AspNetCore.SignalR;
    using ChatApp.Data;
    using Microsoft.EntityFrameworkCore;
    using ChatApp.Models;

    namespace ChatApp.Hubs
    {
        public class ChatHub : Hub
        {
            private readonly ApplicationDbContext _context;

            public ChatHub(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task JoinRoom(string roomId)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
                var messages = await _context.Messages
                    .Where(m => m.ChatRoomId == int.Parse(roomId))
                    .OrderBy(m => m.Timestamp)
                    .Select(m => new { m.UserId, m.Content, m.Timestamp })
                    .ToListAsync();
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveMessageHistory", messages);
            }

            public async Task SendMessage(string roomId, string message)
            {
                var user = Context.UserIdentifier ?? "Anonymous";
                var timestamp = DateTime.UtcNow;
                var newMessage = new Message
                {
                    UserId = user,
                    Content = message,
                    Timestamp = timestamp,
                    ChatRoomId = int.Parse(roomId)
                };
                _context.Messages.Add(newMessage);
                await _context.SaveChangesAsync();
                await Clients.Group(roomId).SendAsync("ReceiveMessage", user, message, timestamp);
            }
        }
    }