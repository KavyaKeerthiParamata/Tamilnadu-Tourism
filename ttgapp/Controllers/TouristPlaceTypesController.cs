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
    public class TouristPlaceTypesController : Controller
    {
        private readonly TTGContext _context;

        public TouristPlaceTypesController(TTGContext context)
        {
            _context = context;
        }

        // GET: TouristPlaceTypes
        public IActionResult Index()
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                ViewBag.LoggedInUserId = loggedInUser;
                List<TouristPlaceType> tptypes = _context.touristPlaceTypes.ToList();
                return View(tptypes);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }


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

        [HttpPost]
        public IActionResult Create(TouristPlaceType tpt)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Types", tpt.TpTypeImage.FileName);
            FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            tpt.TpTypeImage.CopyTo(stream);

            tpt.TpTypeImagePath = @"/images/Types/" + tpt.TpTypeImage.FileName;
            if (ModelState.IsValid)
            {
                _context.touristPlaceTypes.Add(tpt);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(tpt);
            }
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                TouristPlaceType tpt = _context.touristPlaceTypes.Find(Id);
                return View(tpt);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public IActionResult Edit(TouristPlaceType utP)
        {
            TouristPlaceType eT = _context.touristPlaceTypes.Find(utP.TPTypeId);
            var filePath = "";

            if (utP.TpTypeImage != null)
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/Types", utP.TpTypeImage.FileName);
                FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                utP.TpTypeImage.CopyTo(stream);

                eT.TpTypeImagePath = @"/images/Types/" + utP.TpTypeImage.FileName;
            }
            eT.TPTypeName = utP.TPTypeName;
            eT.TpDescription = utP.TpDescription;
            eT.TPTypeStatus = utP.TPTypeStatus;
           

            if (ModelState.IsValid)
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(utP);
            }
        }

        // GET: TouristPlaceTypes/Details/5

        [HttpGet]
        public IActionResult Details(int Id)
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                 TouristPlaceType tpt = _context.touristPlaceTypes.Find(Id);
                return View(tpt);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }



        // GET: TouristPlaceTypes/Edit/5

        // GET: TouristPlaceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var touristPlaceType = await _context.touristPlaceTypes
                .FirstOrDefaultAsync(m => m.TPTypeId == id);
            if (touristPlaceType == null)
            {
                return NotFound();
            }

            return View(touristPlaceType);
        }

        // POST: TouristPlaceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var touristPlaceType = await _context.touristPlaceTypes.FindAsync(id);
            if (touristPlaceType != null)
            {
                _context.touristPlaceTypes.Remove(touristPlaceType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TouristPlaceTypeExists(int id)
        {
            return _context.touristPlaceTypes.Any(e => e.TPTypeId == id);
        }
    }
}
