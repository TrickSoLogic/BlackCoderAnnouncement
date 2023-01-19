using System.Collections.Generic;

namespace BlackCoderAnnouncement.Services
{
    public interface IAnnouncementService<TAnnouncement>
    {
        IEnumerable<TAnnouncement> GetAllAnnouncements();
        IEnumerable<TAnnouncement> SearchAnnouncements(string searchTerm);
        TAnnouncement GetAnnouncementById(int id);
        bool SaveAnnouncement(TAnnouncement announcement);
        bool UpdateAnnouncement(TAnnouncement announcement);
        bool DeleteAnnouncement(TAnnouncement announcement);
    }
}
