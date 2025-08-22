using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    namespace ChatApp.Models
    {
        public class ChatRoom
        {
            public int Id { get; set; }
            [Required, StringLength(100)]
            public required string Name { get; set; }
            public bool IsPrivate { get; set; }
            public List<Message>? Messages { get; set; }
        }
    }