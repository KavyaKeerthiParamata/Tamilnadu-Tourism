using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ttgapp.Dal;
using ttgapp.Models;

namespace ttgapp.Controllers
{
    public class UserController : Controller
    {
        private readonly TTGContext _context;

        public UserController(TTGContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ValidateUser(UserLogin ul)
        {
            if ((!ModelState.IsValid))
            {
                return View("Login");
            }

            User? x = _context.users.FirstOrDefault(u => u.UserId.ToLower() == ul.UserId.ToLower() && u.Password == ul.Password);
            if (x == null)
            {
                ModelState.AddModelError(string.Empty, "Incorrect User ID or Password");
                return View(Login);
            }
            else
            {
                if (x.Status == true)
                {
                    Role r = _context.roles.Find(x.RoleId);
                    if (r != null)
                    {
                        if (r.RoleName == "Admin")
                        {
                            HttpContext.Session.SetString("loggedinuser", ul.UserId);
                            HttpContext.Session.SetString("loggedinuserRole", "Admin");
                            return RedirectToAction("Index", "Admin");
                        }
                        else if (r.RoleName == "Employee1")
                        {
                            HttpContext.Session.SetString("loggedinuser", ul.UserId);
                            HttpContext.Session.SetString("loggedinuserRole", "Employee1");
                            return RedirectToAction("Index", "Employee1");
                        }
                        else if (r.RoleName == "Employee2")
                        {
                            HttpContext.Session.SetString("loggedinuser", ul.UserId);
                            HttpContext.Session.SetString("loggedinuserRole", "Employee2");
                            return RedirectToAction("Index", "Employee2");
                        }
                        else if (r.RoleName == "User")
                        {
                            HttpContext.Session.SetString("loggedinuser", ul.UserId);
                            HttpContext.Session.SetString("loggedinuserRole", "User");
                            return RedirectToAction("Index", "Home"); // or any controller/view you want
                        }

                        else
                        {
                            return View("Login");
                        }
                    }
                    else
                    {
                        return View("Login");
                    }

                }
                else
                {
                    return View("Login");
                }

            }
           
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedinuser");
            HttpContext.Session.Remove("loggedinuserRole");

            return RedirectToAction("Index", "Home");
        }




        [HttpGet]
        public IActionResult Register()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User u)
        {
            if (ModelState.IsValid)
            {
                // Automatically assign default role
                var defaultRole = _context.roles.FirstOrDefault(r => r.RoleName == "User");
                if (defaultRole == null)
                {
                    ModelState.AddModelError("", "Default role not found.");
                    return View(u);
                }

                u.RoleId = defaultRole.RoleId;
                u.Status = true;

                _context.users.Add(u);
                _context.SaveChanges();

                return RedirectToAction("Login", "User");
            }

            return View(u);
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
