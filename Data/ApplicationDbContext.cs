using ChatApp.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    namespace ChatApp.Data
    {
        public class ApplicationDbContext : IdentityDbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<ChatRoom> ChatRooms { get; set; }
            public DbSet<Message> Messages { get; set; }
        }
    }