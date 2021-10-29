using LMS.Data;
using LMS.Models;
using LMS.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Areas.User.Controllers
{
    [Area("User")]
    public class StudentCourseController : Controller
    {
        private readonly ApplicationDbContext db;
        public readonly IWebHostEnvironment webHostEnvironment;
        public StudentCourseController(ApplicationDbContext db, IWebHostEnvironment _webHostEnvironment)
        {
            this.db = db;
            webHostEnvironment = _webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Student user)
        {
            var users = db.students.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
            if (user == null)
            {
                ViewBag.message = "UserName or Password incorrect";

            }
            HttpContext.Session.SetInt32("UserID", users.ID);
            HttpContext.Session.SetString("UserName", users.Name);
            return RedirectToAction(nameof(Index));
        }


        //display course for student 
        public IActionResult Index()
        {
            var UserName = HttpContext.Session.GetString("UserName");
            var CurrentUser =  db.students.Where(x => x.Name == UserName).FirstOrDefault();
            var Course = db.courses.Include(x => x.Teacher).Include(x => x.Student).Include(x=>x.Level).Where(x => x.Student.Name == CurrentUser.Name).ToList();
            return View(Course);
        }

        //display events and actitvity
        public IActionResult ZoneIndex()
        {
             var zone = db.Zones.ToList();
            return View(zone);
        }
        [HttpGet]
        public IActionResult CreateZone()
        {
            StudentVM studentVM = new StudentVM();
          studentVM.Students= db.students.Select(x => new Student { ID = x.ID, Name = x.Name }).ToList();
            return View(studentVM);
        }
        [HttpPost]
        public async Task<ActionResult> CreateZone(StudentVM studentVM, IFormCollection formValues)
        {

            studentVM.Zone.Role = Request.Form["StudentList"];
            string webrootpath = webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var uploads = Path.Combine(webrootpath, "File");

                using (var filesStream = new FileStream(Path.Combine(uploads, files[0].FileName), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                studentVM.Zone.File= @"\File\" + files[0].FileName;
            }

            db.Zones.Add(studentVM.Zone);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(ZoneIndex));
        }


        public IActionResult TimeTableIndex()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }


    }
}
