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
    public class TouristPlacesController : Controller
    {
        private readonly TTGContext _context;

        public TouristPlacesController(TTGContext context)
        {
            _context = context;
        }

        // GET: TouristPlaces
        public async Task<IActionResult> Index()
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                ViewBag.LoggedInUserId = loggedInUser;
                int count = _context.touristPlaces.Count();

                //List<TouristPlace> tpList = await _context.touristPlaces.ToListAsync();
                
                return View(await _context.touristPlaces.Include("touristPlaceType").ToListAsync());
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        // GET: TouristPlaces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var touristPlace = await _context.touristPlaces
                .FirstOrDefaultAsync(m => m.TPId == id);
            if (touristPlace == null)
            {
                return NotFound();
            }

            return View(touristPlace);
        }

        // GET: TouristPlaces/Create
        [HttpGet]
        public IActionResult Create()
        {

            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                ViewBag.LoggedInUserId = loggedInUser;
                var districtList = Enum.GetValues(typeof(District))
                       .Cast<District>()
                       .Select(d => new SelectListItem
                       {
                           Value = d.ToString(),
                           Text = d.ToString()
                       }).ToList();

                ViewBag.Districts = districtList;

                List<SelectListItem> typeList = new List<SelectListItem>();
                foreach(TouristPlaceType tpType  in _context.touristPlaceTypes)
                {
                    typeList.Add(new SelectListItem(tpType.TPTypeName, tpType.TPTypeId.ToString()));
                }
                ViewBag.TPTypeId = typeList;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


        [HttpPost]
        public IActionResult Create(TouristPlace tp)
        {
            if (tp.TPImage != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Places", tp.TPImage.FileName);
                FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                tp.TPImage.CopyTo(stream);

                tp.TPImagePath = @"/images/Places/" + tp.TPImage.FileName;
            }
            if (ModelState.IsValid)
            {
                _context.touristPlaces.Add(tp);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(tp);
            }
        }
        // GET: TouristPlaces/Edit/5
        [HttpGet]
        public IActionResult Edit(int id)
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                ViewBag.LoggedInUserId = loggedInUser;

                TouristPlace tp = _context.touristPlaces.Find(id);
                if (tp == null)
                {
                    return NotFound();
                }

                var districtList = Enum.GetValues(typeof(District))
                    .Cast<District>()
                    .Select(d => new SelectListItem
                    {
                        Value = d.ToString(),
                        Text = d.ToString()
                    }).ToList();
                ViewBag.Districts = districtList;

                List<SelectListItem> typeList = new List<SelectListItem>();
                foreach (TouristPlaceType tpType in _context.touristPlaceTypes)
                {
                    typeList.Add(new SelectListItem(tpType.TPTypeName, tpType.TPTypeId.ToString()));
                }
                ViewBag.TPTypeId = typeList;

                return View(tp); 
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        // POST: TouristPlaces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        public IActionResult Edit(TouristPlace utp)
        {
            TouristPlace eTP = _context.touristPlaces.Find(utp.TPId);
            if (eTP == null)
            {
                return NotFound();
            }

            if (utp.TPImage != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Places", utp.TPImage.FileName);
                using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                utp.TPImage.CopyTo(stream);

                eTP.TPImagePath = @"/images/Places/" + utp.TPImage.FileName;
            }

            // Update other fields
            eTP.TPName = utp.TPName;
            eTP.TPDescription = utp.TPDescription;
            eTP.TPTypeId = utp.TPTypeId;
            eTP.District = utp.District;
            eTP.State = "Tamil Nadu"; // Fixed value
            eTP.Country = "India";    // Fixed value
            eTP.TransportationDetails = utp.TransportationDetails;
            eTP.AccomodationDetails = utp.AccomodationDetails;
            eTP.TPGoogleLink = utp.TPGoogleLink;
            eTP.Location= utp.Location;
            eTP.TPStatus = utp.TPStatus;
            eTP.IsPopular = utp.IsPopular;

            if (ModelState.IsValid)
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                var districtList = Enum.GetValues(typeof(District))
                    .Cast<District>()
                    .Select(d => new SelectListItem
                    {
                        Value = d.ToString(),
                        Text = d.ToString()
                    }).ToList();
                ViewBag.Districts = districtList;

                List<SelectListItem> typeList = new List<SelectListItem>();
                foreach (TouristPlaceType tpType in _context.touristPlaceTypes)
                {
                    typeList.Add(new SelectListItem(tpType.TPTypeName, tpType.TPTypeId.ToString()));
                }
                ViewBag.TPTypeId = typeList;

                return View(utp);
            }
        }

        // GET: TouristPlaces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var touristPlace = await _context.touristPlaces
                .FirstOrDefaultAsync(m => m.TPId == id);
            if (touristPlace == null)
            {
                return NotFound();
            }

            return View(touristPlace);
        }

        // POST: TouristPlaces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var touristPlace = await _context.touristPlaces.FindAsync(id);
            if (touristPlace != null)
            {
                _context.touristPlaces.Remove(touristPlace);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TouristPlaceExists(int id)
        {
            return _context.touristPlaces.Any(e => e.TPId == id);
        }
    }
}
