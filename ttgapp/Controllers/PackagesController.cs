using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ttgapp.Dal;
using ttgapp.Models;
using static System.Net.WebRequestMethods;

namespace ttgapp.Controllers
{
    public class PackagesController : Controller
    {
        private readonly TTGContext _context;

        public PackagesController(TTGContext context)
        {
            _context = context;
        }

        // GET: Packages
        public async Task<IActionResult> Index()
        {

            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                ViewBag.LoggedInUserId = loggedInUser;
                return View(await _context.packages.ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
            
        }

        // GET: Packages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.packages
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // GET: Packages/Create
        // GET: Packages/Create

        [HttpGet]
        public IActionResult Create()
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                ViewBag.LoggedInUserId = loggedInUser;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: Packages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Package package)
        {
            if (package.PackageImage == null)
            {
                ModelState.AddModelError("PackageImage", "Please upload a package image");
            }

            if (!ModelState.IsValid)
            {
                return View(package);
            }

            var folderPath = Path.Combine("wwwroot/images/Packages");
            Directory.CreateDirectory(folderPath);

            var filePath = Path.Combine(folderPath, package.PackageImage.FileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            package.PackageImage.CopyTo(stream);

            package.PackageImagepath = "/images/Packages/" + package.PackageImage.FileName;

            _context.packages.Add(package);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: Packages/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                Package pkg = _context.packages.Find(id);
                return View(pkg);
            }

            return RedirectToAction("Login", "User");
        }


        // POST: Packages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Package up)
        {
            var eP = _context.packages.Find(up.PackageId);
            if (eP == null)
            {
                return NotFound();
            }

            // Save new image if uploaded
            if (up.PackageImage != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Packages");
                Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, up.PackageImage.FileName);
                using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                up.PackageImage.CopyTo(stream);

                eP.PackageImagepath = "/images/Packages/" + up.PackageImage.FileName;
            }
            else
            {
                // Preserve existing image path if no new image uploaded
                up.PackageImagepath = eP.PackageImagepath;
            }

            // Update other fields
            eP.PackageName = up.PackageName;
            eP.PackageDescription = up.PackageDescription;
            eP.PackageVideopath = up.PackageVideopath;
            eP.PackagePrice = up.PackagePrice;
            eP.PackageStatus = up.PackageStatus;

            if (ModelState.IsValid)
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Preserve image path in the view model if validation fails
            up.PackageImagepath = eP.PackageImagepath;
            return View(up);
        }


        // GET: Packages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _context.packages
                .FirstOrDefaultAsync(m => m.PackageId == id);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: Packages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var package = await _context.packages.FindAsync(id);
            if (package != null)
            {
                _context.packages.Remove(package);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackageExists(int id)
        {
            return _context.packages.Any(e => e.PackageId == id);
        }
        public IActionResult PublicPackages()
        {
            var packages = _context.packages
                .Where(p => p.PackageStatus == true)
                .ToList();

            return View(packages);
        }
        public IActionResult PublicDetails(int id)
        {
            var pkg = _context.packages.FirstOrDefault(p => p.PackageId == id && p.PackageStatus == true);
            if (pkg == null)
                return NotFound();

            return View(pkg);
        }

    }
}
