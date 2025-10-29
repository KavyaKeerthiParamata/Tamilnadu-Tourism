using Microsoft.AspNetCore.Mvc;
using ttgapp.Dal;
using ttgapp.Models;

namespace ttgapp.Controllers
{
    public class SearchController : Controller
    {
        private readonly TTGContext _context;

        public SearchController(TTGContext context)
        {
            _context = context;
        }

        public IActionResult Results(string query)
        {
            var model = new SearchResultsViewModel
            {
                Query = query,
                TouristPlaces = _context.touristPlaces
                    .Where(p => p.TPName.Contains(query) || p.TPDescription.Contains(query))
                    .ToList(),
                Packages = _context.packages
                    .Where(p => p.PackageName.Contains(query) || p.PackageDescription.Contains(query))
                    .ToList(),
                ImageGalleries = _context.ImageGallery
                    .Where(g => g.ImageGalleryName.Contains(query))
                    .ToList()
            };

            return View(model);
        }
    }

}

