using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace LMS.Areas.Dashboard.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;
        public readonly IWebHostEnvironment webHostEnvironment;
        public AdminController(ApplicationDbContext db, IWebHostEnvironment _webHostEnvironment)
        {
            this.db = db;
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> register(Register register, IFormCollection formValues)
        {
            string webrootpath = webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var uploads = Path.Combine(webrootpath, "images");
              
                using (var filesStream = new FileStream(Path.Combine(uploads, files[0].FileName), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                register.image = @"\images\" + files[0].FileName ;
            }
            else
            {
                var uploads = Path.Combine(webrootpath, @"images\" + "Avatar.jpg");
                System.IO.File.Copy(uploads, webrootpath + @"\images\" + "Avatar.jpg");
                register.image = @"\images\" + "Avatar.jpg";
            }
            db.registers.Add(register);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public  IActionResult Login(User user)
        {
            var users =  db.registers.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault(); 
            if(user == null)
            {
                ViewBag.message = "UserName or Password incorrect";

            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult CalendarAR()
        {
            return View();
        }


        public IActionResult Profile()
        {
            return View();
        }

        // another Profile 
        public IActionResult Detalis()
        {
            return View();
        }


        public IActionResult Student()
        {
            return View();
        }

        public IActionResult Parent()
        {
            return View();
        }

        public IActionResult Teacher()
        {
            return View();
        }

        public IActionResult HoDs()
        {
            return View();
        }

        public IActionResult admin()
        {
            return View();
        }

        public IActionResult Course()
        {
            return View();
        }

        public IActionResult Premissions()
        {
            return View();
        }

        public IActionResult PremissionsGroups()
        {
            return View();
        }


        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult library()
        {
            return View();
        }

        public IActionResult WeeklyPlans()
        {
            return View();
        }

        public IActionResult Activity()
        {
            return View();
        }

        public IActionResult Attendance()
        {
            return View();
        }


        public IActionResult PassLimit()
        {
            return View();
        }

        public IActionResult GPA()
        {
            return View();
        }


    }

}
