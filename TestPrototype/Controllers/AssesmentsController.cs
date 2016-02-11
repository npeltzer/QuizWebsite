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
    public class AssesmentsController : Controller
    {
        private QuizContext db = new QuizContext();

        // GET: Assesments
        public ActionResult Index()
        {
            return View(db.Assesments.ToList());
        }

        // GET: Assesments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assesment assesment = db.Assesments.Find(id);
            if (assesment == null)
            {
                return HttpNotFound();
            }
            return View(assesment);
        }

        // GET: Assesments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Assesments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Name,Code")] Assesment assesment)
        {
            if (ModelState.IsValid)
            {
                db.Assesments.Add(assesment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(assesment);
        }

        // GET: Assesments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assesment assesment = db.Assesments.Find(id);
            if (assesment == null)
            {
                return HttpNotFound();
            }
            return View(assesment);
        }

        // POST: Assesments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Name,Code")] Assesment assesment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assesment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(assesment);
        }

        // GET: Assesments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Assesment assesment = db.Assesments.Find(id);
            if (assesment == null)
            {
                return HttpNotFound();
            }
            return View(assesment);
        }

        // POST: Assesments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Assesment assesment = db.Assesments.Find(id);
            db.Assesments.Remove(assesment);
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
