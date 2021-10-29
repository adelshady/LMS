using LMS.Data;
using LMS.Models;
using LMS.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace LMS.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext db;
        public readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminController(ApplicationDbContext db, IWebHostEnvironment _webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            webHostEnvironment = _webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        //#region Register

        //[HttpGet]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ActionName("Register")]
        //public async Task<IActionResult> register(Register register, IFormCollection formValues)
        //{
        //    string webrootpath = webHostEnvironment.WebRootPath;
        //    var files = HttpContext.Request.Form.Files;
        //    if (files.Count > 0)
        //    {
        //        var uploads = Path.Combine(webrootpath, "images");

        //        using (var filesStream = new FileStream(Path.Combine(uploads, files[0].FileName), FileMode.Create))
        //        {
        //            files[0].CopyTo(filesStream);
        //        }
        //        register.image = @"\images\" + files[0].FileName;
        //    }
        //    else
        //    {
        //        var uploads = Path.Combine(webrootpath, @"images\" + "Avatar.jpg");
        //        System.IO.File.Copy(uploads, webrootpath + @"\images\" + "Avatar.jpg");
        //        register.image = @"\images\" + "Avatar.jpg";
        //    }
        //    db.registers.Add(register);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        //#endregion
        public IActionResult Index()
        {
            return View();
        }



        #region Login
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
        #endregion


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
                ViewBag.Section = new SelectList(db.stages.Where(X => X.SectionId == stage.SectionId), "ID", "Name");
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
            var StageId = await db.stages.Include(x => x.Section).SingleOrDefaultAsync(x => x.ID == id);
            ViewBag.Section = new SelectList(db.stages.Where(x => x.SectionId == StageId.SectionId), "ID", "Name");
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
                var StageID = await db.stages.Include(x => x.Section).SingleOrDefaultAsync(x => x.ID == id);
                ViewBag.Section = new SelectList(db.stages.Where(x => x.SectionId == StageID.SectionId), "ID", "Name");
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
            var StageId = await db.stages.Include(x => x.Section).SingleOrDefaultAsync(x => x.ID == id);
            ViewBag.Section = new SelectList(db.stages.Where(x => x.SectionId == StageId.SectionId), "ID", "Name");
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
            return View(level);
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
                var ExitedSubSection = db.levels.Include(x => x.Stage).Include(x => x.Stage.Section).Where(x => x.Name == level.Name);
                ViewBag.Stage = new SelectList(db.levels.Where(x => x.StageId == level.StageId), "ID", "Name");
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
            var levelId = await db.levels.Include(x => x.Stage).Include(x => x.Stage.Section).SingleOrDefaultAsync(x => x.ID == id);
            ViewBag.Stage = new SelectList(db.levels.Where(x => x.StageId == levelId.StageId), "ID", "Name");
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
                var ExitedSubSection = db.levels.Include(x => x.Stage).Include(x => x.Stage.Section).Where(x => x.Name == level.Name);
                var levelID = await db.levels.Include(x => x.Stage).Include(x => x.Stage.Section).SingleOrDefaultAsync(x => x.ID == id);
                ViewBag.Stage = new SelectList(db.levels.Where(x => x.StageId == levelID.StageId), "ID", "Name");
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
            var levelId = await db.levels.Include(x => x.Stage).Include(x => x.Stage.Section).SingleOrDefaultAsync(x => x.ID == id);
            ViewBag.Stage = new SelectList(db.levels.Where(x => x.StageId == levelId.StageId), "ID", "Name");
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

        #region Lesson
        public IActionResult LessonIndex()
        {
            var lesson = db.lessons.ToList();
            return View(lesson);
        }
        [HttpGet]
        public IActionResult LessonCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LessonCreate(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                var ExitedLesson = db.lessons.Where(x => x.Name == lesson.Name);
                if (ExitedLesson.Count() > 0)
                {
                    ViewBag.message = "Lesson Name exited please use another name";
                }
                else
                {
                    db.lessons.Add(lesson);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(LessonIndex));
                }
            }
            return View(lesson);
        }
        [HttpGet]
        public IActionResult LessonEdit(int id)
        {
            var lesson = db.lessons.Find(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }
        [HttpPost]
        public async Task<IActionResult> LessonEdit(int id, Lesson lesson)
        {
            db.Update(lesson);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(LessonIndex));
        }

        public IActionResult LessonDetails(int id)
        {
            var LessonId = db.lessons.Find(id);
            return View(LessonId);
        }

        public async Task<IActionResult> LessonDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var LessonId = await db.lessons.FindAsync(id);
            db.lessons.Remove(LessonId);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(LessonIndex));
        }
        #endregion

        #region Chapter

        public IActionResult ChapterIndex()
        {
            var Chapters = db.chapters.Include(x => x.Lesson).ToList();
            return View(Chapters);
        }
        [HttpGet]
        public IActionResult ChapterCreate()
        {
            ViewBag.Lesson = new SelectList(db.lessons, "ID", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChapterCreate(Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                var ExitedChapter = db.chapters.Include(x => x.Lesson).Where(x => x.Name == chapter.Name);
                ViewBag.Lesson = new SelectList(db.lessons, "ID", "Name");
                if (ExitedChapter.Count() > 0)
                {
                    ViewBag.message = "Chapter Name exited please use another name";
                }
                else
                {
                    db.chapters.Add(chapter);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(ChapterIndex));
                }
            }
            return View(chapter);
        }
        [HttpGet]
        public async Task<IActionResult> ChapterEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ChapterId = await db.chapters.Include(x => x.Lesson).SingleOrDefaultAsync(x => x.ID == id);
            ViewBag.Lesson = new SelectList(db.chapters.Where(x => x.LessonId == ChapterId.LessonId), "ID", "Name");
            if (ChapterId == null)
            {
                return NotFound();
            }
            return View(ChapterId);
        }
        [HttpPost]
        public async Task<IActionResult> ChapterEdit(int id, Chapter chapter)
        {
            if (ModelState.IsValid)
            {
                var ExitedChapterId = db.chapters.Include(x => x.Lesson).Where(x => x.Name == chapter.Name);
                var ChapterId = await db.chapters.Include(x => x.Lesson).SingleOrDefaultAsync(x => x.ID == id);
                ViewBag.Lesson = new SelectList(db.chapters.Where(x => x.LessonId == ChapterId.LessonId), "ID", "Name");
                if (ExitedChapterId.Count() > 0)
                {
                    ViewBag.message = "Chapter Name exited please use another name";
                }
                else
                {
                    var ChapterID = await db.chapters.FindAsync(id);
                    ChapterID.Name = chapter.Name;
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(ChapterIndex));
                }
            }
            return View(chapter);
        }
        public async Task<IActionResult> ChapterDetalis(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ChapterId = await db.chapters.Include(x => x.Lesson).SingleOrDefaultAsync(x => x.ID == id);
            ViewBag.Lesson = new SelectList(db.lessons, "ID", "Name");
            return View(ChapterId);
        }
        public async Task<IActionResult> ChapterDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ChapterID = await db.chapters.FindAsync(id);

            db.chapters.Remove(ChapterID);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(StageIndex));
        }

        #endregion

        #region Events
        public IActionResult EventIndex()
        {
            var events = db.events.ToList();
            return View(events);
        }
        public IActionResult EventCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EventCreate(Events events)
        {
            var ExitedEvents = db.events.Where(x => x.Name == events.Name);

            if (ExitedEvents.Count() > 0)
            {
                ViewBag.message = "Events exited please use another name";
            }
            else
            {
                db.events.Add(events);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(EventIndex));
            }
            return View(events);
        }
        public IActionResult EventEdit(int id)
        {
            var EventID = db.events.Find(id);
            return View(EventID);
        }
        [HttpPost]
        public async Task<IActionResult> EventEdit(Events events)
        {
            db.events.Update(events);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(EventIndex));
        }

        public async Task<IActionResult> EventDelete(int id)
        {
            var EventsID = db.events.Find(id);
            db.events.Remove(EventsID);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(EventIndex));
        }
        #endregion

        //#region Tasks
        //public IActionResult TaskIndex()
        //{
        //    var TasksList = db.tasks.ToList();
        //    return View(TasksList);
        //}
        //public IActionResult TaskCreate()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> TaskCreate(Tasks tasks)
        //{
        //    var ExitedTasks = db.tasks.Where(x => x.Name == tasks.Name);

        //    if (ExitedTasks.Count() > 0)
        //    {
        //        ViewBag.message = "Tasks exited please use another name";
        //    }
        //    else
        //    {
        //        db.tasks.Add(tasks);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction(nameof(TaskIndex));
        //    }
        //    return View(tasks);
        //}
        //public IActionResult TaskEdit(int id)
        //{
        //    var TaskID = db.tasks.Find(id);
        //    return View(TaskID);
        //}
        //[HttpPost]
        //public async Task<IActionResult> TaskEdit(Tasks tasks)
        //{
        //    db.tasks.Update(tasks);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction(nameof(TaskIndex));
        //}

        //public async Task<IActionResult> TaskDelete(int id)
        //{
        //    var TasksID = db.tasks.Find(id);
        //    db.tasks.Remove(TasksID);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction(nameof(TaskIndex));
        //}
        //#endregion

        #region Chat
        public async Task<IActionResult> Chat()
        {

            var UserName = HttpContext.Session.GetString("UserName");
            var CurrentUser = await db.users.Where(x => x.Name == UserName).FirstOrDefaultAsync();
            ViewBag.CurrentUserName = CurrentUser.Name;

            var message = await db.message.Where(x => x.FromUserId == CurrentUser.ID).ToListAsync();
            return View(message);
        }
        [HttpPost]
        public async Task<IActionResult> CreateChat(Message message)
        {
            var UserID = HttpContext.Session.GetInt32("UserID");
            var UserName = HttpContext.Session.GetString("UserName");
            var CurrentUser = await db.users.Where(x => x.Name == UserName).FirstOrDefaultAsync();

            message.DateInserted = DateTime.Now;
            message.ToUserId = (int)UserID;
            message.FromUserId = CurrentUser.ID;
            await db.message.AddAsync(message);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Chat));
        }

        #endregion

        #region Calendar
        public IActionResult CalendarIndex()
        {
            var calendar = db.calendars.Include(x => x.Level).Include(x => x.Events).ToList();
            return View(calendar);
        }

        public IActionResult CalendarCreate()
        {
            List<CheckBoxItem> lstchk = new List<CheckBoxItem>()
            {
                new CheckBoxItem {TasksId =1 ,Name="Teacher"},
                new CheckBoxItem {TasksId =2 ,Name="HoDs"},
                 new CheckBoxItem {TasksId =3 ,Name="Student"},
                new CheckBoxItem {TasksId =4 ,Name="Parent"}
            };

            var model = new CalendarAndTasks
            {
                checkBoxItems = lstchk
            };

            ViewBag.Events = new SelectList(db.events, "ID", "Name");
            ViewBag.Level = new SelectList(db.levels, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            return View(model);
        }
        [HttpPost]
        public IActionResult CalendarCreate(CalendarAndTasks calendarAndTasks)
        {

            calendarAndTasks.calendar.CategoryIds = Request.Form["CategoryIds"];
            calendarAndTasks.calendar.LevelId = Convert.ToInt32(Request.Form["Level"]);
            var Listlevel = db.levels.Find(calendarAndTasks.calendar.LevelId);
            calendarAndTasks.calendar.Level = Listlevel;


            var day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(calendarAndTasks.Day);
            if (day == DayOfWeek.Saturday)
            {
                calendarAndTasks.calendar.day = "Saturday";

            }
            else if (day == DayOfWeek.Sunday)
            {
                calendarAndTasks.calendar.day = "Sunday";
            }
            else if (day == DayOfWeek.Monday)
            {
                calendarAndTasks.calendar.day = "Monday";
            }
            else if (day == DayOfWeek.Tuesday)
            {
                calendarAndTasks.calendar.day = "Tuesday";
            }
            else if (day == DayOfWeek.Thursday)
            {
                calendarAndTasks.calendar.day = "Thursday";
            }
            else if (day == DayOfWeek.Wednesday)
            {
                calendarAndTasks.calendar.day = "Wednesday";
            }
            else if (day == DayOfWeek.Friday)
            {
                calendarAndTasks.calendar.day = "Friday";
            }
          

            if (string.IsNullOrEmpty(calendarAndTasks.calendar.CategoryIds))  //this is used when no checkbox is checked
            {
                calendarAndTasks.calendar.CategoryIds = "None,None";
            }

            db.calendars.Add(calendarAndTasks.calendar);
            db.SaveChanges();

            return RedirectToAction(nameof(CalendarIndex));
        }

        public IActionResult CalendarEdit(int id)
        {
            List<CheckBoxItem> lstchk = new List<CheckBoxItem>()
            {
                new CheckBoxItem {TasksId =1 ,Name="Teacher"},
                new CheckBoxItem {TasksId =2 ,Name="HoDs"},
                 new CheckBoxItem {TasksId =3 ,Name="Student"},
                new CheckBoxItem {TasksId =4 ,Name="Parent"}
             };
            var categorylist = db.calendars.FirstOrDefault();
            var model = new CalendarAndTasks
            {
                calendar = db.calendars.Include(x => x.Level).Include(x => x.Events).Include(x => x.Level.Stage).Include(x => x.Level.Stage.Section).Where(x => x.ID == id).FirstOrDefault(),

                checkBoxItems = lstchk,
                CategoryIds = categorylist.CategoryIds.Split(',')/*.Split(',')*///here get your comma separated list from database and assign it to the CategoryIds string array, i have used sample text for the values
            };
            var SectionID = model.calendar.Level.Stage.SectionId;
            var StageID = model.calendar.Level.StageId;
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages.Where(c => c.SectionId == SectionID).ToList(), "ID", "Name");
            ViewBag.Level = new SelectList(db.levels.Where(c => c.StageId == StageID).ToList(), "ID", "Name");
            ViewBag.Events = new SelectList(db.events, "ID", "Name");
            return View(model);
        }
        [HttpPost]
        public IActionResult CalendarEdit(CalendarAndTasks calendarAndTasks)
        {
            calendarAndTasks.calendar.CategoryIds = Request.Form["CategoryIds"];
            calendarAndTasks.calendar.LevelId = Convert.ToInt32(Request.Form["Level"]);
            var Listlevel = db.levels.Find(calendarAndTasks.calendar.LevelId);
            calendarAndTasks.calendar.Level = Listlevel;
            if (string.IsNullOrEmpty(calendarAndTasks.calendar.CategoryIds))  //this is used when no checkbox is checked
            {
                calendarAndTasks.calendar.CategoryIds = "None,None";
            }
            db.calendars.Add(calendarAndTasks.calendar);
            db.SaveChanges();
            return RedirectToAction(nameof(CalendarIndex));
        }

        #endregion

        #region Announcement

        public IActionResult AnnounceIndex()
        {
            var announce = db.Announcements.Include(x => x.Level).ToList(); 
            return View(announce);
        }

        public IActionResult AnnounceCreate()
        {
            List<CheckBoxItem> lstchk = new List<CheckBoxItem>()
            {
                new CheckBoxItem {TasksId =1 ,Name="Teacher"},
                new CheckBoxItem {TasksId =2 ,Name="HoDs"},
                 new CheckBoxItem {TasksId =3 ,Name="Student"},
                new CheckBoxItem {TasksId =4 ,Name="Parent"}
            };

            var model = new AnnounceVM
            {
                checkBoxItems = lstchk
            };

            ViewBag.Level = new SelectList(db.levels, "ID", "Name");
            ViewBag.Stage = new SelectList(db.stages, "ID", "Name");
            ViewBag.Section = new SelectList(db.sections, "ID", "Name");
            return View(model);
        }
        [HttpPost]
        public IActionResult AnnounceCreate(AnnounceVM  announceVM)
        {

            announceVM.Announcement.Roles = Request.Form["CategoryIds"];
            announceVM.Announcement.LevelId = Convert.ToInt32(Request.Form["Level"]);
            var Listlevel = db.levels.Find(announceVM.Announcement.LevelId);
            announceVM.Announcement.Level = Listlevel;

            announceVM.Announcement.CreatedAt = DateTime.Now;

            if (string.IsNullOrEmpty(announceVM.Announcement.Roles))  //this is used when no checkbox is checked
            {
                announceVM.Announcement.Roles= "None,None"; 
            }

            db.Announcements.Add(announceVM.Announcement);
            db.SaveChanges();

            return RedirectToAction(nameof(AnnounceIndex));
        }

        #endregion

        public IActionResult Profile()
        {
            return View();
        }

        // another Profile 
        public IActionResult DetalisP()
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


        //public IActionResult Chat()
        //{
        //    return View();
        //}

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
