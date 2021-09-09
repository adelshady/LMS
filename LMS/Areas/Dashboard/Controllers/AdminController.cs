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
            var users =  db.users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault(); 
            if(user == null)
            {
                ViewBag.message = "UserName or Password incorrect";

            }
            return RedirectToAction(nameof(Index));
        }

        #region Section
        
        public IActionResult SectionIndex()
        {
            var section = db.sections.ToList();
            return View(section);
        }
        [HttpGet]
        [ActionName("Create")]
        public IActionResult SectionCreate()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> SectionCreate(Section section)
        {
            if (ModelState.IsValid)
            {
                var ExitedSubSection = db.sections.Where(x => x.Name == section.Name);

                if (ExitedSubSection.Count() > 0)
                {
                    ViewBag.message = "Section exited please use another name";
                }
                else
                {
                    db.sections.Add(section);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(SectionIndex));
                }
            }
            return View(section);
        }
        [HttpGet]
        [ActionName("Edit")]
        public async Task<IActionResult> SectionEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var SectionId = await db.sections.FindAsync(id);
            if (SectionId == null)
            {
                return NotFound();
            }
            return View(SectionId);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> SectionEdit(Section section)
        {
            if (ModelState.IsValid)
            {
                db.Update(section);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(SectionIndex));
            }
            return View(section);
        }

        public async Task<IActionResult> Detalis(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var SectionId = await db.sections.FindAsync(id);
            return View(SectionId);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var sectionId = await db.sections.FindAsync(id);

            db.sections.Remove(sectionId);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(SectionIndex));
        }

        #endregion


        #region Stage
        public IActionResult StageIndex()
        {
            var section = db.stages.Include(x => x.Section).ToList();
            return View(section);
        }
        [HttpGet]
        public IActionResult StageCreate()
        {
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StageCreate(Stage stage)
        {
            if (ModelState.IsValid)
            {
                var ExitedSubSection = db.stages.Include(x => x.Section).Where(x => x.Name == stage.Name);
                ViewBag.Section = new SelectList(db.sections, "ID", "Name");
                if (ExitedSubSection.Count() > 0)
                {
                    ViewBag.message = "Section exited please use another name";
                }
                else
                {
                    db.stages.Add(stage);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(StageIndex));
                }
            }
            return View(stage);
        }
        [HttpGet]
        public async Task<IActionResult> StageEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var StageId = await db.stages.Include(x=>x.Section).SingleOrDefaultAsync(x=>x.ID==id);
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            if (StageId == null)
            {
                return NotFound();
            }
            return View(StageId);
        }
        [HttpPost]
        public async Task<IActionResult> StageEdit(int id, Stage stage)
        {
            if (ModelState.IsValid)
            {
                var ExitedSubSection = db.stages.Include(x => x.Section).Where(x => x.Name == stage.Name);
                ViewBag.Section = new SelectList(db.sections, "ID", "Name");
                if (ExitedSubSection.Count() > 0)
                {
                    ViewBag.message = "Section exited please use another name";
                }
                else
                {
                    var StageId = await db.stages.FindAsync(id);
                    StageId.Name = stage.Name;
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(StageIndex));
                }
            }
            return View(stage);
        }
        public async Task<IActionResult> StageDetalis(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var StageId = await db.stages.Include(x=>x.Section).SingleOrDefaultAsync(x=>x.ID==id);
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            return View(StageId);
        }
        public async Task<IActionResult> StageDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var StageId = await db.stages.FindAsync(id);

            db.stages.Remove(StageId);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(StageIndex));
        }
        #endregion


        #region Level
          public IActionResult LevelIndex()
        {
            var level = db.levels.Include(x => x.Stage).Include(x => x.Stage.Section).ToList();
            return View(level) ;
        }
        [HttpGet]
        public IActionResult LevelCreate()
        {
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LevelCreate(Level level)
        {
            if (ModelState.IsValid)
            {
                var ExitedSubSection = db.levels.Include(x => x.Stage).Include(x=>x.Stage.Section).Where(x => x.Name == level.Name);
                ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
                if (ExitedSubSection.Count() > 0)
                {
                    ViewBag.message = "Level Name exited please use another name";
                }
                else
                {
                    db.levels.Add(level);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(StageIndex));
                }
            }
            return View(level);
        }
        public async Task<IActionResult> LevelEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var levelId = await db.levels.Include(x => x.Stage).Include(x=>x.Stage.Section).SingleOrDefaultAsync(x => x.ID == id);
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            if (levelId == null)
            {
                return NotFound();
            }
            return View(levelId);
        }
        [HttpPost]
        public async Task<IActionResult> LevelEdit(int id, Level level)
        {
            if (ModelState.IsValid)
            {
                var ExitedSubSection = db.levels.Include(x => x.Stage).Include(x=>x.Stage.Section).Where(x => x.Name == level.Name);
                ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
                if (ExitedSubSection.Count() > 0)
                {
                    ViewBag.message = "Level Name exited please use another name";
                }
                else
                {
                    var levelId = await db.levels.FindAsync(id);
                    levelId.Name = level.Name;
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(StageIndex));
                }
            }
            return View(level);
        }

        public async Task<IActionResult> LevelDetalis(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var levelId = await db.levels.Include(x => x.Stage).Include(x=>x.Stage.Section).SingleOrDefaultAsync(x => x.ID == id);
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            return View(levelId);
        }
        public async Task<IActionResult> LevelDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var LevelId = await db.levels.FindAsync(id);

            db.levels.Remove(LevelId);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(StageIndex));
        }

        #endregion

        public IActionResult CalendarAR()
        {
            return View();
        }


        public IActionResult Profile()
        {
            return View();
        }

        // another Profile 
        public IActionResult DetalisP()
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
