using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkplaceOfSecretary.DAL;
using WorkplaceOfSecretary.Models;

namespace WorkplaceOfSecretary.Controllers
{
    public class DataForListController : Controller
    {
        private WoSContext db = new WoSContext();

        public ActionResult Create()
        {
            List<SEB> sebs = new List<SEB>();
            sebs.Add(null);
            sebs.AddRange(db.SEBs.ToList());

            ViewBag.SebID = new SelectList(sebs, "ID", "NameOfSEB");

            List<Group> groups = new List<Group>();
            groups.Add(null);
            groups.AddRange(db.Groups.ToList());

            ViewBag.GroupID = new SelectList(groups, "ID", "NumberOfGroup");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DataForList dataForList)
        {
            if (ModelState.IsValid)
            {
                string SEB = db.SEBs.Where(s => s.ID == dataForList.SebID).FirstOrDefault().NameOfSEB;
                string date = dataForList.Date.Day + " " + GetMonth(dataForList) + " " + dataForList.Date.Year;
                string group = db.Groups.Where(g => g.ID == dataForList.GroupID).FirstOrDefault().NumberOfGroup;
                string specialty = dataForList.Specialty;
                List<Student> students = db.Groups.Where(g => g.ID == dataForList.GroupID).FirstOrDefault().Students.OrderBy(s => s.LastName).ToList();

                Application app = new Application();
                Document doc = app.Documents.Add(@"F:\Список.docx");
                doc.Bookmarks["SEB"].Range.Text = SEB;
                doc.Bookmarks["Date"].Range.Text = date;
                doc.Bookmarks["Group"].Range.Text = group;
                doc.Bookmarks["Specialty"].Range.Text = specialty;

                for(int i = 0; i < students.Count; i++)
                {
                    string fullName = students[i].LastName + " " + students[i].FirstName + " " + students[i].Patronymic;
                    doc.Tables[1].Rows.Add();

                    int number = doc.Tables[1].Rows.Count - 1;

                    doc.Tables[1].Cell(doc.Tables[1].Rows.Count, 1).Range.Text = number.ToString();
                    doc.Tables[1].Cell(doc.Tables[1].Rows.Count, 2).Range.Text = fullName;
                    doc.Tables[1].Cell(doc.Tables[1].Rows.Count, 3).Range.Text = students[i].AverageScore.ToString();
                }

                doc.SaveAs(FileName: @"F:\NewСписок.docx");
                app.Documents.Open(@"F:\NewСписок.docx");
                try
                {
                    //doc.Close();
                    //app.Quit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return RedirectToAction("Index", "Home");
            }

            return View(dataForList);
        }

        // Получение номера и наименования специальности по id группы
        public ActionResult GetSpecialty(int? id)
        {

            DataForList dataForList = new DataForList();

            if (id != null)
            {
                Group group = db.Groups.Where(g => g.ID == id).FirstOrDefault();

                Specialty specialty = db.Specialties.Where(s => s.ID == group.SpecialtyID).FirstOrDefault();

                dataForList.Specialty = specialty.NumberOfSpecialty + "- «" + specialty.NameOfSpecialty + "»";
            }

            return PartialView(dataForList);
        }

        // Получить список студентов по id группы
        public ActionResult GetStudents(int? id)
        {
            Group group = new Group();

            if(id != null)
            {
                group = db.Groups.Where(g => g.ID == id).FirstOrDefault();
            }

            return PartialView(group);
        }

        public string GetMonth(DataForList dataForList)
        {
            string[] months = { "января", "февраля", "марта", "апреля", "мая", "июня",
                "июля", "августа", "сентября", "октября", "ноября", "декабря" };

            int i = dataForList.Date.Month;

            return (months[i]);
        }
    }
}