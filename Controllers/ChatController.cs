using Microsoft.AspNetCore.Mvc;
    using ChatApp.Data;
    using ChatApp.Models;
    using Microsoft.EntityFrameworkCore;

    namespace ChatApp.Controllers
    {
        [Route("api/Chat")]
        [ApiController]
        public class ChatController : ControllerBase
        {
            private readonly ApplicationDbContext _context;

            public ChatController(ApplicationDbContext context)
            {
                _context = context;
            }

            [HttpGet("rooms")]
            public async Task<IActionResult> GetRooms()
            {
                var rooms = await _context.ChatRooms.ToListAsync();
                return Ok(rooms);
            }

            [HttpPost("rooms")]
            public async Task<IActionResult> CreateRoom([FromBody] ChatRoom room)
            {
                _context.ChatRooms.Add(room);
                await _context.SaveChangesAsync();
                return Ok(room);
            }

            [HttpGet("messages/{roomId}")]
            public async Task<IActionResult> GetMessages(int roomId)
            {
                var messages = await _context.Messages
                    .Where(m => m.ChatRoomId == roomId)
                    .OrderBy(m => m.Timestamp)
                    .ToListAsync();
                return Ok(messages);
            }
        }
    }