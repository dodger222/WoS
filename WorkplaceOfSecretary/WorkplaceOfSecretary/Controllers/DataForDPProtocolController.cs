using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkplaceOfSecretary.DAL;
using WorkplaceOfSecretary.Models;

namespace WorkplaceOfSecretary.Controllers
{
    public class DataForDPProtocolController : Controller
    {
        private WoSContext db = new WoSContext();
        
        public ActionResult Create()
        {
            List<Group> groups = new List<Group>();

            groups.Add(null);

            groups.AddRange(db.Groups.ToList());

            ViewBag.GroupID = new SelectList(db.Groups, "ID", "NumberOfGroup");

            return View();
        }

        // Получение списка студентов по id группы
        public ActionResult GetStudents(int id)
        {
            List<FullNameStudent> fullNameStudents  = new List<FullNameStudent>();

            foreach(var item in db.Students.Where(s => s.GroupID == id))
            {
                FullNameStudent fullNameStudent = new FullNameStudent();

                fullNameStudent.ID = item.ID;
                fullNameStudent.LastName = item.LastName;
                fullNameStudent.FirstName = item.FirstName;
                fullNameStudent.Patronymic = item.Patronymic;
                fullNameStudent.FullName = item.LastName + " " + item.FirstName + " " + item.Patronymic;

                fullNameStudents.Add(fullNameStudent);
            }

            return PartialView(fullNameStudents);
        }

        // Получение фамилии студента по id студента
        public ActionResult GetLastNameStudent(int? id)
        {
            Student student = new Student();

            if (id != null)
            {
                student = db.Students.Where(s => s.ID == id).FirstOrDefault();
            }

            return PartialView(student);
        }

        // Получение имени студента по id студента
        public ActionResult GetFirstNameStudent(int? id)
        {
            Student student = new Student();

            if (id != null)
            {
                student = db.Students.Where(s => s.ID == id).FirstOrDefault();
            }

            return PartialView(student);
        }

        // Получение отчества студента по id студента
        public ActionResult GetPatronymicStudent(int? id)
        {
            Student student = new Student();

            if (id != null)
            {
                student = db.Students.Where(s => s.ID == id).FirstOrDefault();
            }

            return PartialView(student);
        }

    }
}