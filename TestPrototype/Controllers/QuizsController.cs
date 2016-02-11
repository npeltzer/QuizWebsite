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
    public class QuizsController : Controller
    {
        private QuizContext db = new QuizContext();
        public List<QuizQuestion> CreateQuiz(int length, Topic top)
        {
            List<MCQuestion> questions = db.MCQuestions.Where(q=>q.QuestionTopic.id== top.id).ToList();
            if (questions.Count < length)
                return null;
            List<QuizQuestion> ret = new List<QuizQuestion>();
            Random rand = new Random();
            var numsUsed = new List<int>();
            while (numsUsed.Count < length)
            {
                int r = rand.Next(0, questions.Count);

                if (!numsUsed.Contains(r))
                {
                    var toAdd = new QuizQuestion();
                    toAdd.question = questions.ElementAt(r);
                    toAdd.UserAnswer = Answer.NA;
                    db.QuizQuestions.Add(toAdd);
                    ret.Add(toAdd);
                    numsUsed.Add(r);
                }

            }

            return ret;

        }
        // GET: Quizs
        public ActionResult Index()
        {
            var quizs = db.Quizs.Include(q => q.QuestionTopic);
            return View(quizs.ToList());
        }
        // GET: Quizs/Take/5
        public ActionResult Take(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
           
            return View(quiz);
        }

        // POST: Quizs/Take/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Take([Bind(Include = "id,TopicId,QuizLength")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicId = new SelectList(db.Topics, "id", "Title", quiz.TopicId);
            return View(quiz);
        }
        // GET: Quizs/Score/5
        public ActionResult Score(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            double correct = 0.0; //new List<Boolean>();
            foreach (var q in quiz.Questions)
            {
                if (q.UserAnswer == q.question.CorrectAnswer)
                    correct++;

                //correct.Add(q.UserAnswer == q.question.CorrectAnswer);

            }

            ViewBag.correct = (correct / quiz.QuizLength)*100;
            return View(quiz);
        }
        // GET: Quizs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // GET: Quizs/Create
        public ActionResult Create()
        {
            ViewBag.TopicId = new SelectList(db.Topics, "id", "Title");
            return View();
        }

        // POST: Quizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,TopicId,QuizLength")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {

                var top = db.Topics.Find(quiz.TopicId);
                quiz.QuestionTopic = top;
                top.Quizzes.Add(quiz);
                quiz.Questions = CreateQuiz(quiz.QuizLength,top);
                db.Quizs.Add(quiz);
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TopicId = new SelectList(db.Topics, "id", "Title", quiz.TopicId);
            return View(quiz);
        }

        // GET: Quizs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            ViewBag.TopicId = new SelectList(db.Topics, "id", "Title", quiz.TopicId);
            return View(quiz);
        }

        // POST: Quizs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,TopicId,QuizLength")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TopicId = new SelectList(db.Topics, "id", "Title", quiz.TopicId);
            return View(quiz);
        }

        // GET: Quizs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quiz quiz = db.Quizs.Find(id);
            if (quiz == null)
            {
                return HttpNotFound();
            }
            return View(quiz);
        }

        // POST: Quizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quiz quiz = db.Quizs.Find(id);
            db.QuizQuestions.RemoveRange(quiz.Questions);
            db.Quizs.Remove(quiz);
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
