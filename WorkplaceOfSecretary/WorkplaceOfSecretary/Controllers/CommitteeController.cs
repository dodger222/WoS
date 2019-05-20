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
    public class CommitteeController : Controller
    {
        private WoSContext db = new WoSContext();

        // GET: Committee
        public ActionResult Index()
        {
            var committees = db.Committees.Include(c => c.Chairperson)
                .Include(c => c.MemberOne)
                .Include(c => c.MemberThree)
                .Include(c => c.MemberTwo)
                .Include(c => c.Seb)
                .Include(c => c.Secretary);
            return View(committees.ToList());
        }

        // GET: Committee/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }
            return View(committee);
        }

        // GET: Committee/Create
        public ActionResult Create()
        {
            ViewBag.ChairpersonID = new SelectList(db.Employees, "ID", "LastName");
            ViewBag.MemberOneID = new SelectList(db.Employees, "ID", "LastName");
            ViewBag.MemberThreeID = new SelectList(db.Employees, "ID", "LastName");
            ViewBag.MemberTwoID = new SelectList(db.Employees, "ID", "LastName");
            ViewBag.SebID = new SelectList(db.SEBs, "ID", "NameOfSEB");
            ViewBag.SecretaryID = new SelectList(db.Employees, "ID", "LastName");
            return View();
        }

        // POST: Committee/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Committee committee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Committees.Add(committee);
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
            ViewBag.ChairpersonID = new SelectList(db.Employees, "ID", "LastName", committee.ChairpersonID);
            ViewBag.MemberOneID = new SelectList(db.Employees, "ID", "LastName", committee.MemberOneID);
            ViewBag.MemberThreeID = new SelectList(db.Employees, "ID", "LastName", committee.MemberThreeID);
            ViewBag.MemberTwoID = new SelectList(db.Employees, "ID", "LastName", committee.MemberTwoID);
            ViewBag.SebID = new SelectList(db.SEBs, "ID", "NameOfSEB", committee.SebID);
            ViewBag.SecretaryID = new SelectList(db.Employees, "ID", "LastName", committee.SecretaryID);
            return View(committee);
        }

        // GET: Committee/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChairpersonID = new SelectList(db.Employees, "ID", "LastName", committee.ChairpersonID);
            ViewBag.MemberOneID = new SelectList(db.Employees, "ID", "LastName", committee.MemberOneID);
            ViewBag.MemberThreeID = new SelectList(db.Employees, "ID", "LastName", committee.MemberThreeID);
            ViewBag.MemberTwoID = new SelectList(db.Employees, "ID", "LastName", committee.MemberTwoID);
            ViewBag.SebID = new SelectList(db.SEBs, "ID", "NameOfSEB", committee.SebID);
            ViewBag.SecretaryID = new SelectList(db.Employees, "ID", "LastName", committee.SecretaryID);
            return View(committee);
        }

        // POST: Committee/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SebID,ChairpersonID,SecretaryID,MemberOneID,MemberTwoID,MemberThreeID")] Committee committee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(committee).State = EntityState.Modified;
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
            ViewBag.ChairpersonID = new SelectList(db.Employees, "ID", "LastName", committee.ChairpersonID);
            ViewBag.MemberOneID = new SelectList(db.Employees, "ID", "LastName", committee.MemberOneID);
            ViewBag.MemberThreeID = new SelectList(db.Employees, "ID", "LastName", committee.MemberThreeID);
            ViewBag.MemberTwoID = new SelectList(db.Employees, "ID", "LastName", committee.MemberTwoID);
            ViewBag.SebID = new SelectList(db.SEBs, "ID", "NameOfSEB", committee.SebID);
            ViewBag.SecretaryID = new SelectList(db.Employees, "ID", "LastName", committee.SecretaryID);
            return View(committee);
        }

        // GET: Committee/Delete/5
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
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }
            return View(committee);
        }

        // POST: Committee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Committee committee = db.Committees.Find(id);
                db.Committees.Remove(committee);
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
