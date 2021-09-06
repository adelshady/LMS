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

namespace LMS.Areas.Dashboard.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext db;
        public readonly IWebHostEnvironment webHostEnvironment;
        public StudentController(ApplicationDbContext db, IWebHostEnvironment _webHostEnvironment)
        {
            this.db = db;
            webHostEnvironment = _webHostEnvironment;
        }
        public IActionResult Index()
        {
            var user = db.users.Include(x => x.Level).Where(x => x.JobType == "Student").ToList();
            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.level = new SelectList(db.levels, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user, IFormCollection formValues)
        {
            user.LevelId = Convert.ToInt32(Request.Form["Level"]);
            var Listlevel = db.levels.Find(user.LevelId);
            user.Level = Listlevel;
            string role = Request.Form["JobType"];
            user.JobType = role;
            string webrootpath = webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var uploads = Path.Combine(webrootpath, "images");

                using (var filesStream = new FileStream(Path.Combine(uploads, files[0].FileName), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                user.image = @"\images\" + files[0].FileName;
            }
            else
            {
                var uploads = Path.Combine(webrootpath, @"images\" + "Avatar.jpg");
                System.IO.File.Copy(uploads, webrootpath + @"\images\" + "Avatar.jpg");
                user.image = @"\images\" + "Avatar.jpg";
            }
            db.users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int ID)
        {
            var user = db.users.Include(x => x.Level).Include(x => x.Level.Stage).Include(x => x.Level.Stage.Section).Where(x => x.ID == ID).SingleOrDefault();
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            ViewBag.Level = new SelectList(db.levels, "ID", "Name");
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int ID, IFormCollection formValuses, User users)
        {
            var user = await db.users.Include(x => x.Level).Include(x => x.Level.Stage).Include(x => x.Level.Stage.Section).Where(x => x.ID == ID).SingleOrDefaultAsync();
            users.LevelId = Convert.ToInt32(Request.Form["Level"]);
            var Listlevel = db.levels.Find(users.LevelId);
            user.Level = Listlevel;

            string webrootpath = webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if (files.Count > 0)
            {
                var uploads = Path.Combine(webrootpath, "images");

                var imagepath = Path.Combine(webrootpath, user.image.TrimStart('\\'));

                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (var filesStream = new FileStream(Path.Combine(uploads, files[0].FileName), FileMode.Create))
                {
                    files[0].CopyTo(filesStream);
                }
                user.image = @"\images\" + files[0].FileName;
            }
            user.Name = users.Name;
            user.NationalId = users.NationalId;
            user.ParentName = users.ParentName;
            user.phone = users.phone;
            user.status = users.status;
            user.LevelId = users.LevelId;
            user.Level.StageId = users.Level.StageId;
            user.Level.Stage.SectionId = users.Level.Stage.SectionId;
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

      
        public IActionResult Details(int ID)
        {
            var user = db.users.Include(x => x.Level).Include(x => x.Level.Stage).Include(x => x.Level.Stage.Section).Where(x => x.ID == ID).SingleOrDefault();
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            ViewBag.Level = new SelectList(db.levels, "ID", "Name");
            return View(user);
        }


        public async Task<IActionResult> Delete(int ID)
        {
            var user = db.users.Include(x => x.Level).Include(x => x.Level.Stage).Include(x => x.Level.Stage.Section).Where(x => x.ID == ID).SingleOrDefault();
            string webRootPath = webHostEnvironment.WebRootPath;

            var imagePath = Path.Combine(webRootPath, user.image.TrimStart('\\'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            db.users.Remove(user);
            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetStage(int id)
        {
            List<Stage> subCategories = new List<Stage>();
            subCategories = await (from stage in db.stages
                                   where stage.SectionId == id
                                   select stage).ToListAsync();
            return Json(new SelectList(subCategories, "ID", "Name"));
        }

        public async Task<IActionResult> GetLevel(int id)
        {
            List<Level> Level = new List<Level>();
            Level = await (from section in db.levels
                           where section.StageId == id
                           select section).ToListAsync();
            return Json(new SelectList(Level, "ID", "Name"));
        }

    }
}
