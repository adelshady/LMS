using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Areas.Instructor.Controllers
{
    [Area("Instructor")]
    public class TeacherCourseController : Controller
    {
        private readonly ApplicationDbContext db;
        public readonly IWebHostEnvironment webHostEnvironment;
        public TeacherCourseController(ApplicationDbContext db, IWebHostEnvironment _webHostEnvironment)
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
        public IActionResult Login(Teacher user)
        {
            var users = db.teachers.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();
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
            var CurrentUser = db.students.Where(x => x.Name == UserName).FirstOrDefault();
            var Course = db.courses.Include(x => x.Teacher).Include(x => x.Student).Include(x => x.Level).Where(x => x.Student.Name == CurrentUser.Name).ToList();
            return View(Course);
        }

        public IActionResult ZoneIndex()
        {
            var zone = db.Zones.ToList();
            return View(zone);
        }

        public IActionResult IndexContent()
        {
            var Content = db.Contents.ToList();
            return View(Content);
        }

        [HttpGet]
        public IActionResult CreateContent()
        {
            ViewBag.Chapter = new SelectList(db.chapters, "ID", "Name");
            ViewBag.Lesson = new SelectList(db.lessons, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateContent(Content content, [FromForm] IFormFile file)
        {
            var chapter = db.chapters.Find(content.ChapterId);
            content.Chapter.Name = chapter.Name;
            //var Lesson = db.lessons.Find(content.Chapter.LessonId);
            //content.Chapter.Lesson.Name = Lesson.Name;


            using (var dataStream = new MemoryStream())
            {
                await file.CopyToAsync(dataStream);
                content = new Content
                {
                    ID = content.ID,
                    ContentName = content.ContentName,
                    VideoData = dataStream.ToArray(),
                    Body= content.Body,
                    PublishDate = content.PublishDate,
                    ChapterId=content.ChapterId
                };

                db.Contents.Add(content);
                await db.SaveChangesAsync();

                return RedirectToAction(nameof(IndexContent));
            }


        }
        public IActionResult SampleVideoStream(int id)
        {
            var data = db.Contents.Find(id);
            return File(data.VideoData, "video/mp4");
        }
    }
}