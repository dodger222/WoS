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

            ViewBag.GroupID = new SelectList(groups, "ID", "NumberOfGroup");

            List<SEB> sebs = new List<SEB>();
            sebs.Add(null);
            sebs.AddRange(db.SEBs.ToList());

            ViewBag.SebID = new SelectList(sebs, "ID", "NameOfSEB");

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

        // Получение номера и наименования специальности по id группы
        public ActionResult GetSpecialty(int? id)
        {
            vSpecialty vSpecialty = new vSpecialty();

            if (id != null)
            {
                Group group = db.Groups.Where(g => g.ID == id).FirstOrDefault();

                Specialty specialty = db.Specialties.Where(s => s.ID == group.SpecialtyID).FirstOrDefault();

                vSpecialty.ID = specialty.ID;
                vSpecialty.Specialty = specialty.NumberOfSpecialty + " " + specialty.NameOfSpecialty;
            }

            return PartialView(vSpecialty);
        }

        // Получение председателя комиссии по id ГЭКа
        public ActionResult GetChairperson(int? id)
        {
            vEmployee chairperson = new vEmployee();

            if(id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                Employee employee = new Employee();
                employee = db.Employees.Where(e => e.ID == committee.ChairpersonID).FirstOrDefault();

                chairperson.ID = employee.ID;
                chairperson.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                chairperson.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                chairperson.ShortFullNameOne = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." +employee.LastName;
            }

            return PartialView(chairperson);
        }

    }
}