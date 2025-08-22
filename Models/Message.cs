using System.ComponentModel.DataAnnotations;

    namespace ChatApp.Models
    {
        public class Message
        {
            public int Id { get; set; }
            [Required]
            public required string UserId { get; set; }
            [Required]
            public required string Content { get; set; }
            public DateTime Timestamp { get; set; }
            public int ChatRoomId { get; set; }
            public ChatRoom? ChatRoom { get; set; }
        }
    }