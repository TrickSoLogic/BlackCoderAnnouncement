using Microsoft.AspNetCore.Mvc;
using BlackCoderAnnouncement.Models;
using BlackCoderAnnouncement.Services;
using BlackCoderAnnouncement.ViewModels;

namespace BlackCoderAnnouncement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnnouncementService<Announcement> _dataRepository;
        public HomeController(IAnnouncementService<Announcement> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public ActionResult Index(string searchTerm)
        {
            var model = new AnnouncementsListingModel
            {
                SearchTerm = searchTerm, Announcements = _dataRepository.SearchAnnouncements(searchTerm)
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? id)
        {
            var model = new AnnouncementActionModel();

            // Trying to edit an announcement
            if (id.HasValue)
            {
                var announcement = _dataRepository.GetAnnouncementById(id.Value);

                model.Id = announcement.Id;
                model.Title = announcement.Title;
                model.Description = announcement.Description;
            }

            return PartialView("_Action", model);
        }

        /// <summary>
        /// Action for edit or create an announcement.
        /// </summary>
        /// <param name="model">Input data about announcement.</param>
        /// <returns>Result is json that creates or modifies model.</returns>
        [HttpPost]
        public JsonResult Action(AnnouncementActionModel model)
        {
            var json = new JsonResult(new { });
            bool result;

            // Trying to edit an announcement
            if (model.Id > 0)
            {
                var announcement = _dataRepository.GetAnnouncementById(model.Id);
                announcement.Title = model.Title;
                announcement.Description = model.Description;

                result = _dataRepository.UpdateAnnouncement(announcement);
            }
            // Trying to create an announcement
            else
            {
                var announcement = new Announcement
                {
                    Title = model.Title, Description = model.Description
                };

                result = _dataRepository.SaveAnnouncement(announcement);
            }

            if (result)
            {
                json.Value = new { Success = true };
            }
            else
            {
                json.Value = new { Success = false, Message = "Unable to perform action on Announcement." };
            }

            return json;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = new AnnouncementActionModel();

            var announcement = _dataRepository.GetAnnouncementById(id);
            model.Id = announcement.Id;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AnnouncementActionModel model)
        {
            var json = new JsonResult(new { });

            var announcement = _dataRepository.GetAnnouncementById(model.Id);
            var result = _dataRepository.DeleteAnnouncement(announcement);

            if (result)
            {
                json.Value = new { Success = true };
            }
            else
            {
                json.Value = new { Success = false, Message = "Unable to perform action on Announcement." };
            }

            return json;
        }
    }
}
