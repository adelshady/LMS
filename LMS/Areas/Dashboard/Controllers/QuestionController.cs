using LMS.Data;
using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Areas.Dashboard.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ApplicationDbContext db;
       
        public QuestionController(ApplicationDbContext db)
        {
            this.db = db;
        
        }
        #region QuestionType
        public IActionResult QuestionTypeIndex()
        {
            var QuestionType = db.QuestionType.ToList();
            return View(QuestionType);
        }

        public IActionResult QuestionTypeCreate()
        {          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> QuestionTypeCreate(QuestionType questionType)
        {
            if (ModelState.IsValid)
            {
                var ExitedSubSection = db.sections.Where(x => x.Name == questionType.Name);

                if (ExitedSubSection.Count() > 0)
                {
                    ViewBag.message = "QuestionType Name exited please use another name";
                }
                else
                {
                    db.QuestionType.Add(questionType);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(QuestionTypeIndex));
                }
            }
            return View(questionType);
        }

        public IActionResult QuestionTypeDelete(int id)
        {
            var Question = db.QuestionType.Find(id);
            db.QuestionType.Remove(Question);
            db.SaveChanges();
            return RedirectToAction(nameof(QuestionTypeIndex));
        }
        #endregion


        public IActionResult QuestionIndex()
        {
            var Question = db.Questions.Include(x => x.QuestionType).ToList();
            return View(Question);
        }

        public IActionResult QuestionCreate()
        {
            ViewBag.Question = new SelectList(db.QuestionType, "ID", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> QuestionCreate(Question question)
        {
            ViewBag.Question = new SelectList(db.Questions.Where(X => X.QuestionTypeId== question.QuestionTypeId), "ID", "Name");
            db.Questions.Add(question);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(QuestionIndex));
               
           
        }
    }
}
