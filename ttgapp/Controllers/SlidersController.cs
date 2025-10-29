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
    public class SlidersController : Controller
    {
        private readonly TTGContext _context;

        public SlidersController(TTGContext context)
        {
            _context = context;
        }

        // GET: Sliders
        public IActionResult Index()
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if(loggedInUser !=null && loggedinuserRole=="Admin")
            {
                ViewBag.LoggedInUserId = loggedInUser;
                List<Slider> sliderList = _context.sliders.ToList();
                return View(sliderList);
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
        public IActionResult Create(Slider s)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders", s.SliderImage.FileName);
            FileStream stream=new FileStream(filePath, FileMode.Create, FileAccess.Write);
            s.SliderImage.CopyTo(stream);

            s.SliderImagePath = @"/images/sliders/" + s.SliderImage.FileName;
            if(ModelState.IsValid)
            {
                _context.sliders.Add(s);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(s);
            }
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                Slider s = _context.sliders.Find(Id);
                return View(s);
            }
            else
            {
                return RedirectToAction("Login","User");
            }
        }

        [HttpPost]
        public IActionResult Edit(Slider upS)
        {
            Slider eS = _context.sliders.Find(upS.SliderId);
            var filePath = "";

            if(upS.SliderImage != null)
            {
                filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/sliders", upS.SliderImage.FileName);
                FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                upS.SliderImage.CopyTo(stream);

                eS.SliderImagePath = @"/images/sliders/" + upS.SliderImage.FileName;
            }
            eS.Name = upS.Name;
            eS.DisplayText = upS.DisplayText;
            eS.LinkText = upS.LinkText;
            eS.Status = upS.Status;
            eS.DisplayOrderNo = upS.DisplayOrderNo;

            if(ModelState.IsValid)
            {
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(upS);
            }
        }

        [HttpGet]
        public IActionResult Details(int Id)
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if (loggedInUser != null && loggedinuserRole == "Admin")
            {
                Slider s = _context.sliders.Find(Id);
                return View(s);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }

        }

        public IActionResult Delete(int Id)
        {
            Slider s=_context.sliders.Find(Id);
            _context.sliders.Remove(s);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        
    }
}
