using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestPrototype.Models;

namespace TestPrototype.Controllers
{
    public class MCQuestionsController : Controller
    {
        private QuizContext db = new QuizContext();

        // GET: MCQuestions
        public ActionResult Index()
        {
            return View(db.MCQuestions.ToList());
        }

        // GET: MCQuestions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCQuestion mCQuestion = db.MCQuestions.Find(id);
            if (mCQuestion == null)
            {
                return HttpNotFound();
            }
            return View(mCQuestion);
        }

        // GET: MCQuestions/Create
        public ActionResult Create()
        {
            ViewBag.Topics = db.Topics.ToList();
            return View();
        }

        // POST: MCQuestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Question,A,B,C,D,CorrectAnswer,TopicId")] MCQuestion mCQuestion)
        {
            if (ModelState.IsValid)
            {
              var top=db.Topics.Find(mCQuestion.TopicId);
                mCQuestion.QuestionTopic = top;
                top.Questions.Add(mCQuestion);
                db.MCQuestions.Add(mCQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Topics = db.Topics.ToList();
            return View(mCQuestion);
        }

        // GET: MCQuestions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCQuestion mCQuestion = db.MCQuestions.Find(id);
            if (mCQuestion == null)
            {
                return HttpNotFound();
            }
            return View(mCQuestion);
        }

        // POST: MCQuestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Question,A,B,C,D,CorrectAnswer")] MCQuestion mCQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mCQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mCQuestion);
        }

        // GET: MCQuestions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MCQuestion mCQuestion = db.MCQuestions.Find(id);
            if (mCQuestion == null)
            {
                return HttpNotFound();
            }
            return View(mCQuestion);
        }

        // POST: MCQuestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MCQuestion mCQuestion = db.MCQuestions.Find(id);
            mCQuestion.QuestionTopic.Questions.Remove(mCQuestion);
            db.MCQuestions.Remove(mCQuestion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
