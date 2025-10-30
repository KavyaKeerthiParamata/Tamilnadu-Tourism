using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ttgapp.Dal;
using ttgapp.Models;

namespace ttgapp.Controllers
{
    public class ImageGalleryController : Controller
    {
        private readonly TTGContext _context;

        public ImageGalleryController(TTGContext context)
        {
            _context = context;
        }

        // GET: ImageGallery
        public IActionResult Index()
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                var galleries = _context.ImageGallery.Include(g => g.TouristPlace).ToList();
                return View(galleries);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // GET: ImageGallery/Create
        [HttpGet]
        public IActionResult Create()
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                ViewBag.TouristPlaces = _context.touristPlaces
                    .Select(tp => new SelectListItem
                    {
                        Value = tp.TPId.ToString(),
                        Text = tp.TPName
                    }).ToList();

                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: ImageGallery/Create
        [HttpPost]
        public IActionResult Create(ImageGallery gallery)
        {
            if (ModelState.IsValid)
            {
                _context.ImageGallery.Add(gallery);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TouristPlaces = _context.touristPlaces
                .Select(tp => new SelectListItem
                {
                    Value = tp.TPId.ToString(),
                    Text = tp.TPName
                }).ToList();

            return View(gallery);
        }

        // GET: ImageGallery/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                var gallery = _context.ImageGallery.Find(id);
                ViewBag.TouristPlaces = _context.touristPlaces
                    .Select(tp => new SelectListItem
                    {
                        Value = tp.TPId.ToString(),
                        Text = tp.TPName
                    }).ToList();

                return View(gallery);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: ImageGallery/Edit/5
        [HttpPost]
        public IActionResult Edit(ImageGallery updated)
        {
            var existing = _context.ImageGallery.Find(updated.ImageGalleryId);
            if (existing == null)
            {
                return NotFound();
            }

            existing.ImageGalleryName = updated.ImageGalleryName;
            existing.ImageGalleryFolderPath = updated.ImageGalleryFolderPath;
            existing.ImageGalleryStatus = updated.ImageGalleryStatus;
            existing.TPId = updated.TPId;

            if (ModelState.IsValid)
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TouristPlaces = _context.touristPlaces
                .Select(tp => new SelectListItem
                {
                    Value = tp.TPId.ToString(),
                    Text = tp.TPName
                }).ToList();

            return View(updated);
        }

        // GET: ImageGallery/Delete/5
        public IActionResult Delete(int id)
        {
            var gallery = _context.ImageGallery.Find(id);
            return View(gallery);
        }

        // POST: ImageGallery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var gallery = _context.ImageGallery.Find(id);
            if (gallery != null)
            {
                _context.ImageGallery.Remove(gallery);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: ImageGallery/Details/5
        public IActionResult Details(int id)
        {
            var gallery = _context.ImageGallery
                .Include(g => g.TouristPlace)
                .FirstOrDefault(g => g.ImageGalleryId == id);

            return View(gallery);
        }
    }
}
