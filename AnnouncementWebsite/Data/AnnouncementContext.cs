using BlackCoderAnnouncement.Models;
using Microsoft.EntityFrameworkCore;

namespace BlackCoderAnnouncement.Data
{
    public class AnnouncementContext : DbContext
    {
        public AnnouncementContext(DbContextOptions<AnnouncementContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Announcement> Announcements { get; set; }
    }
}
