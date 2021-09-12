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
    public class HoDsController : Controller
    {
        private readonly ApplicationDbContext db;
        public readonly IWebHostEnvironment webHostEnvironment;
        public HoDsController(ApplicationDbContext db, IWebHostEnvironment _webHostEnvironment)
        {
            this.db = db;
            webHostEnvironment = _webHostEnvironment;
        }
        // GET: HoDsController
        public ActionResult Index()
        {
            var HoDs = db.users.Include(x => x.Level).Include(x=>x.Level.Stage).Include(x=>x.Level.Stage.Section).Where(x => x.JobType == "HoDs").ToList();
            return View(HoDs);
        }

        // GET: HoDsController/Details/5
        public ActionResult Details(int ID)
        {
            var user = db.users.Include(x => x.Level).Include(x => x.Level.Stage).Include(x => x.Level.Stage.Section).Where(x => x.ID == ID).SingleOrDefault();
            var SectionID = user.Level.Stage.SectionId;
            var StageID = user.Level.StageId;
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages.Where(c => c.SectionId == SectionID).ToList(), "ID", "Name");
            ViewBag.Level = new SelectList(db.levels.Where(c => c.StageId == StageID).ToList(), "ID", "Name");
            return View(user);
        }

        // GET: HoDsController/Create
        public ActionResult Create()
        {
            ViewBag.level = new SelectList(db.levels, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            return View();
        }

        // POST: HoDsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection , User user)
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
                System.IO.File.Copy(uploads, webrootpath + @"\images\" + user.NationalId + ".jpg");
                user.image = @"\images\" + user.NationalId + ".jpg";
            }
            db.users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: HoDsController/Edit/5
        public ActionResult Edit(int ID)
        {
            var user = db.users.Include(x => x.Level).Include(x => x.Level.Stage).Include(x => x.Level.Stage.Section).Where(x => x.ID == ID).SingleOrDefault();
            var SectionID = user.Level.Stage.SectionId;
            var StageID = user.Level.StageId;
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages.Where(c => c.SectionId == SectionID).ToList(), "ID", "Name");
            ViewBag.Level = new SelectList(db.levels.Where(c => c.StageId == StageID).ToList(), "ID", "Name");
            return View(user);
        }

        // POST: HoDsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Edit(int ID, IFormCollection collection,User users)
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

        // GET: HoDsController/Delete/5
        public ActionResult Delete(int ID)
        {
            var user = db.users.Include(x => x.Level).Include(x => x.Level.Stage).Include(x => x.Level.Stage.Section).Where(x => x.ID == ID).SingleOrDefault();
            return View(user);
        }

        // POST: HoDsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int ID, IFormCollection collection)
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
    }
}
