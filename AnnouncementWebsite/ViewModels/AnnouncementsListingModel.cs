using System.Collections.Generic;
using BlackCoderAnnouncement.Models;

namespace BlackCoderAnnouncement.ViewModels
{
    public class AnnouncementsListingModel
    {
        public IEnumerable<Announcement> Announcements { get; set; }
        public string SearchTerm { get; set; }
    }
}
