using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Hosting;
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
    public class QuizController : Controller
    {
        private readonly ApplicationDbContext db;
        public readonly IWebHostEnvironment webHostEnvironment;
        public QuizController(ApplicationDbContext db,IWebHostEnvironment _webHostEnvironment)
        {
            this.db = db;
            webHostEnvironment = _webHostEnvironment;
        }

        #region Create Quiz
        public IActionResult QuizIndex()
        {
            var Quiz = db.Quiz.ToList();
            return View(Quiz);
        }
        [HttpGet]
        public IActionResult QuizCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> QuizCreate(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                var ExitedSubSection = db.sections.Where(x => x.Name == quiz.Name);

                if (ExitedSubSection.Count() > 0)
                {
                    ViewBag.message = "Quiz Name exited please use another name";
                }
                else
                {
                    db.Quiz.Add(quiz);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(QuizIndex));
                }
            }
            return View(quiz);
        }
        [HttpGet]
        public async Task<IActionResult> QuizEdit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var QuizId = await db.Quiz.FindAsync(Id);
            if (QuizId == null)
            {
                return NotFound();
            }
            return View(QuizId);
        }
        [HttpPost]
        public async Task<IActionResult> QuizEdit(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Update(quiz);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(QuizIndex));
            }
            return View(quiz);
        }
        public async Task<IActionResult> QuizDetalis(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var QuizId = await db.Quiz.FindAsync(Id);
            return View(QuizId);
        }

        public async Task<IActionResult> QuizDelete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var QuizId = await db.Quiz.FindAsync(Id);

            db.Quiz.Remove(QuizId);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(QuizIndex));
        }
        #endregion

        #region QuizDetails
        public IActionResult QuizDetailsIndex()
        {
            var QuizDetails = db.QuizDetails.Include(x => x.Chapter).ToList();
            return View(QuizDetails);
        }
        [HttpGet]
        public IActionResult QuizDetailsCreate()
        {
            ViewBag.Chapter = new SelectList(db.chapters, "ID", "Name");
            ViewBag.Lesson = new SelectList(db.lessons, "ID", "Name");
            ViewBag.QuestionType = new SelectList(db.QuestionType, "ID", "Name"); 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> QuizDetailsCreate(QuizDetails quizDetails)
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
                quizDetails.Question.Image = @"\images\" + files[0].FileName;
            }
            db.QuizDetails.Add(quizDetails);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(QuizDetailsIndex));
        }

        public async Task<IActionResult> getLesson(int id)
        {
            List<Chapter> chapters = new List<Chapter>();
            chapters = await (from chapter in db.chapters
                              where chapter.LessonId == id
                              select chapter).ToListAsync();
            return Json(new SelectList(chapters, "ID", "Name"));
        }
        #endregion


    }
}
