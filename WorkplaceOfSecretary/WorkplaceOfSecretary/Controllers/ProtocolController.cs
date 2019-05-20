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
    public class ProtocolController : Controller
    {
        private WoSContext db = new WoSContext();

        // GET: Protocol
        public ActionResult Index()
        {
            var protocols = db.Protocols.Include(p => p.Leader)
                .Include(p => p.Meeting)
                .Include(p => p.Student);
            return View(protocols.ToList());
        }

        // GET: Protocol/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Protocol protocol = db.Protocols.Find(id);
            if (protocol == null)
            {
                return HttpNotFound();
            }
            return View(protocol);
        }

        // GET: Protocol/Create
        public ActionResult Create()
        {
            ViewBag.LeaderID = new SelectList(db.Employees, "ID", "LastName");
            ViewBag.MeetingID = new SelectList(db.Meetings, "ID", "ID");
            ViewBag.StudentID = new SelectList(db.Students, "ID", "LastName");
            return View();
        }

        // POST: Protocol/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Protocol protocol)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Protocols.Add(protocol);
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
            ViewBag.LeaderID = new SelectList(db.Employees, "ID", "LastName", protocol.LeaderID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "ID", "ID", protocol.MeetingID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "LastName", protocol.StudentID);
            return View(protocol);
        }

        // GET: Protocol/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Protocol protocol = db.Protocols.Find(id);
            if (protocol == null)
            {
                return HttpNotFound();
            }
            ViewBag.LeaderID = new SelectList(db.Employees, "ID", "LastName", protocol.LeaderID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "ID", "ID", protocol.MeetingID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "LastName", protocol.StudentID);
            return View(protocol);
        }

        // POST: Protocol/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,StudentID,LastNameInDative,LastNameInGenitive,FirstNameInGenitive,PatronymicInGenitive,LeaderID,Theme,Consultants,MeetingID")] Protocol protocol)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(protocol).State = EntityState.Modified;
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
            ViewBag.LeaderID = new SelectList(db.Employees, "ID", "LastName", protocol.LeaderID);
            ViewBag.MeetingID = new SelectList(db.Meetings, "ID", "ID", protocol.MeetingID);
            ViewBag.StudentID = new SelectList(db.Students, "ID", "LastName", protocol.StudentID);
            return View(protocol);
        }

        // GET: Protocol/Delete/5
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
            Protocol protocol = db.Protocols.Find(id);
            if (protocol == null)
            {
                return HttpNotFound();
            }
            return View(protocol);
        }

        // POST: Protocol/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Protocol protocol = db.Protocols.Find(id);
                db.Protocols.Remove(protocol);
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
