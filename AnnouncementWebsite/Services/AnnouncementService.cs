using System.Collections.Generic;
using System.Linq;
using BlackCoderAnnouncement.Data;
using BlackCoderAnnouncement.Models;
using Microsoft.EntityFrameworkCore;

namespace BlackCoderAnnouncement.Services
{
    public class AnnouncementService : IAnnouncementService<Announcement>
    {
        private readonly AnnouncementContext _announcementContext;
        public AnnouncementService(AnnouncementContext context)
        {
            _announcementContext = context;
        }
        public IEnumerable<Announcement> GetAllAnnouncements()
        {
            return _announcementContext.Announcements.ToList();
        }

        /// <summary>
        /// Search and take top 3 similar announcements.
        /// </summary>
        /// <param name="searchTerm">Input search string.</param>
        /// <returns>List of announcements.</returns>
        public IEnumerable<Announcement> SearchAnnouncements(string searchTerm)
        {
            var announcements = _announcementContext.Announcements.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                // For showing top 3 similar announcements
                announcements = announcements.Where(x => x.Title.ToLower().Contains(searchTerm.ToLower()))
                                             .Where(x => x.Description.ToLower().Contains(searchTerm.ToLower()))
                                             .Take(3);
            }

            return announcements.ToList();
        }

        public Announcement GetAnnouncementById(int id)
        {
            return _announcementContext.Announcements.Find(id);
        }

        public bool SaveAnnouncement(Announcement announcement)
        {
            _announcementContext.Announcements.Add(announcement);

            return _announcementContext.SaveChanges() > 0;
        }

        public bool UpdateAnnouncement(Announcement announcement)
        {
            _announcementContext.Entry(announcement).State = EntityState.Modified;

            return _announcementContext.SaveChanges() > 0;
        }
        public bool DeleteAnnouncement(Announcement announcement)
        {
            _announcementContext.Entry(announcement).State = EntityState.Deleted;

            return _announcementContext.SaveChanges() > 0;
        }
    }
}
