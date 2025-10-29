using Microsoft.AspNetCore.Mvc;

namespace ttgapp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            string loggedInUser = HttpContext.Session.GetString("loggedinuser");
            string loggedinuserRole = HttpContext.Session.GetString("loggedinuserRole");

            if(loggedInUser != null && loggedinuserRole == "Admin")
            {
                ViewBag.LoggedInUserId=loggedInUser;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
           
        }
    }
}
