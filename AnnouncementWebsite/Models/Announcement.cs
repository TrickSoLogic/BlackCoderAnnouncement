using System;

namespace BlackCoderAnnouncement.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }

        public Announcement()
        {
            DateAdded = DateTime.Now;
        }
    }
}
