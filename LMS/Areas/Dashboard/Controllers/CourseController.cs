using LMS.Data;
using LMS.Models;
using LMS.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Areas.Dashboard.Controllers
{
    public class CourseController : Controller
    {
        private readonly ApplicationDbContext db;
        public CourseController(ApplicationDbContext db)
        {
            this.db = db;
        }
        // GET: CourseController
        public ActionResult Index()
        {
            var courses = db.courses.Include(x => x.Level).Include(x => x.Level.Stage).Include(x => x.Level.Stage.Section)
                .Include(x => x.HoDs).Include(x=>x.Teacher).ToList();
            return View(courses);
        }

        // GET: CourseController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            ViewBag.level = new SelectList(db.levels, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            //TeacherAndHoDsVm teacherAndHoDsVm = new TeacherAndHoDsVm();
            //teacherAndHoDsVm.GetTeacherList = db.teachers.Select(x => new TeacherAndHoDsVm
            //{
            //    TeacherId = x.ID,
            //    TeacherName= x.Name
            //}).ToList();

            var user = db.courses.Include(x => x.HoDs).Include(x=>x.Teacher).ToList();
            return View(user);
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Course course)
        {

            course.Teacher.LevelId = Convert.ToInt32(Request.Form["Level"]);
            var Listlevel = db.levels.Find(course.Teacher.LevelId);
            course.Teacher.Level = Listlevel;

            db.courses.Add(course);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CourseController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CourseController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CourseController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
