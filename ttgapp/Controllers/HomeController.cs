using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ttgapp.Models;
using ttgapp.Services;

namespace ttgapp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DBServices dbs = new DBServices();
            List<TouristPlace> popularPlaces = dbs.GetPopularTouristPlaces();
            return View(popularPlaces); // Pass popular places to the view
        }


        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        public IActionResult TouristPlacesByType(int typeId)
        {
            DBServices dbs = new DBServices();
            var places = dbs.GetTouristPlacesByType(typeId);
            var typeName = dbs.GetAllTouristPlaceTypes()
                              .FirstOrDefault(t => t.TPTypeId == typeId)?.TPTypeName;

            ViewBag.TypeName = typeName;
            return View(places);
        }
        public IActionResult TouristPlaceTypes()
        {
            DBServices dbs = new DBServices();
            List<TouristPlaceType> tptypesList = dbs.GetAllTouristPlaceTypes();
            return View(tptypesList); // This will use a new view
        }


    }
}
