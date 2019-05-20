using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkplaceOfSecretary.DAL;
using WorkplaceOfSecretary.Models;

namespace WorkplaceOfSecretary.Controllers
{
    public class MeetingController : Controller
    {
        private WoSContext db = new WoSContext();

        // GET: Meeting
        public ActionResult Index()
        {
            var meetings = db.Meetings.Include(m => m.Seb);
            return View(meetings.ToList());
        }

        // GET: Meeting/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // GET: Meeting/Create
        public ActionResult Create()
        {
            ViewBag.SebID = new SelectList(db.SEBs, "ID", "NameOfSEB");
            return View();
        }

        // POST: Meeting/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Meeting meeting)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Meetings.Add(meeting);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            ViewBag.SebID = new SelectList(db.SEBs, "ID", "NameOfSEB", meeting.SebID);
            return View(meeting);
        }

        // GET: Meeting/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            ViewBag.SebID = new SelectList(db.SEBs, "ID", "NameOfSEB", meeting.SebID);
            return View(meeting);
        }

        // POST: Meeting/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SebID,Date")] Meeting meeting)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(meeting).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            ViewBag.SebID = new SelectList(db.SEBs, "ID", "NameOfSEB", meeting.SebID);
            return View(meeting);
        }

        // GET: Meeting/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            Meeting meeting = db.Meetings.Find(id);
            if (meeting == null)
            {
                return HttpNotFound();
            }
            return View(meeting);
        }

        // POST: Meeting/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Meeting meeting = db.Meetings.Find(id);
                db.Meetings.Remove(meeting);
                db.SaveChanges();
            }
            catch (DataException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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
