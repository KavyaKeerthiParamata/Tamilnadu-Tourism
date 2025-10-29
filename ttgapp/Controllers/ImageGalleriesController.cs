using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ttgapp.Dal;
using ttgapp.Models;

namespace ttgapp.Controllers
{
    public class ImageGalleriesController : Controller
    {
        private readonly TTGContext _context;

        public ImageGalleriesController(TTGContext context)
        {
            _context = context;
        }

        // GET: ImageGalleries
        public async Task<IActionResult> Index()
        {
            var tTGContext = _context.ImageGallery.Include(i => i.TouristPlace);
            return View(await tTGContext.ToListAsync());
        }

        // GET: ImageGalleries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageGallery = await _context.ImageGallery
                .Include(i => i.TouristPlace)
                .FirstOrDefaultAsync(m => m.ImageGalleryId == id);
            if (imageGallery == null)
            {
                return NotFound();
            }

            return View(imageGallery);
        }

        // GET: ImageGalleries/Create
        public IActionResult Create()
        {
            ViewData["TPId"] = new SelectList(_context.touristPlaces, "TPId", "AccomodationDetails");
            return View();
        }

        // POST: ImageGalleries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImageGalleryId,ImageGalleryName,ImageGalleryFolderPath,ImageGalleryStatus,TPId")] ImageGallery imageGallery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(imageGallery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TPId"] = new SelectList(_context.touristPlaces, "TPId", "AccomodationDetails", imageGallery.TPId);
            return View(imageGallery);
        }

        // GET: ImageGalleries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageGallery = await _context.ImageGallery.FindAsync(id);
            if (imageGallery == null)
            {
                return NotFound();
            }
            ViewData["TPId"] = new SelectList(_context.touristPlaces, "TPId", "AccomodationDetails", imageGallery.TPId);
            return View(imageGallery);
        }

        // POST: ImageGalleries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImageGalleryId,ImageGalleryName,ImageGalleryFolderPath,ImageGalleryStatus,TPId")] ImageGallery imageGallery)
        {
            if (id != imageGallery.ImageGalleryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageGallery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageGalleryExists(imageGallery.ImageGalleryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TPId"] = new SelectList(_context.touristPlaces, "TPId", "AccomodationDetails", imageGallery.TPId);
            return View(imageGallery);
        }

        // GET: ImageGalleries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imageGallery = await _context.ImageGallery
                .Include(i => i.TouristPlace)
                .FirstOrDefaultAsync(m => m.ImageGalleryId == id);
            if (imageGallery == null)
            {
                return NotFound();
            }

            return View(imageGallery);
        }

        // POST: ImageGalleries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var imageGallery = await _context.ImageGallery.FindAsync(id);
            if (imageGallery != null)
            {
                _context.ImageGallery.Remove(imageGallery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageGalleryExists(int id)
        {
            return _context.ImageGallery.Any(e => e.ImageGalleryId == id);
        }
    }
}
