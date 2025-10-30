using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ttgapp.Dal;
using ttgapp.Models;

namespace ttgapp.Controllers
{
    public class VirtualTourController : Controller
    {
        private readonly TTGContext _context;

        public VirtualTourController(TTGContext context)
        {
            _context = context;
        }

        // USER SIDE: List of active virtual tours
        public IActionResult Index()
        {
            var tours = _context.VirtualTour
                .Include(v => v.TouristPlace)
                .Where(v => v.IsActive)
                .ToList();

            return View(tours);
        }

        // USER SIDE: Details of a single tour
        public IActionResult Details(int id)
        {
            var tour = _context.VirtualTour
                .Include(v => v.TouristPlace)
                .FirstOrDefault(v => v.VirtualTourId == id && v.IsActive);

            if (tour == null)
                return NotFound();

            return View(tour);
        }

        // ADMIN SIDE: List all tours
        public IActionResult AdminIndex()
        {
            var tours = _context.VirtualTour.Include(v => v.TouristPlace).ToList();
            return View(tours);
        }

        // ADMIN SIDE: Create new tour
        public IActionResult Create()
        {
            ViewData["TouristPlaces"] = _context.touristPlaces.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VirtualTour tour)
        {
            if (ModelState.IsValid)
            {
                _context.VirtualTour.Add(tour);
                _context.SaveChanges();
                return RedirectToAction(nameof(AdminIndex));
            }

            ViewData["TouristPlaces"] = _context.touristPlaces.ToList();
            return View(tour);
        }

        // ADMIN SIDE: Edit tour
        public IActionResult Edit(int id)
        {
            var tour = _context.VirtualTour.Find(id);
            if (tour == null)
                return NotFound();

            ViewData["TouristPlaces"] = _context.touristPlaces.ToList();
            return View(tour);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, VirtualTour tour)
        {
            if (id != tour.VirtualTourId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(tour);
                _context.SaveChanges();
                return RedirectToAction(nameof(AdminIndex));
            }

            ViewData["TouristPlaces"] = _context.touristPlaces.ToList();
            return View(tour);
        }

        // ADMIN SIDE: Delete tour
        public IActionResult Delete(int id)
        {
            var tour = _context.VirtualTour.Find(id);
            if (tour == null)
                return NotFound();

            return View(tour);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var tour = _context.VirtualTour.Find(id);
            if (tour != null)
            {
                _context.VirtualTour.Remove(tour);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(AdminIndex));
        }
    }
}
