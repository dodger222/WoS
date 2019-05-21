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

        // Получение полного имени председателя комиссии по id ГЭКа
        public ActionResult GetFullNameChairperson(int? id)
        {
            vEmployee chairperson = new vEmployee();

            if(id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.ChairpersonID).FirstOrDefault();

                    chairperson.ID = employee.ID;
                    chairperson.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    chairperson.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    chairperson.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(chairperson);
        }

        // Получение фамилии и инициалов председателя комиссии по id ГЭКа
        public ActionResult GetShortNameChairpersonOne(int? id)
        {
            vEmployee chairperson = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.ChairpersonID).FirstOrDefault();

                    chairperson.ID = employee.ID;
                    chairperson.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    chairperson.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    chairperson.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(chairperson);
        }

        // Получение инициалов и фамилии председателя комиссии по id ГЭКа
        public ActionResult GetShortNameChairpersonTwo(int? id)
        {
            vEmployee chairperson = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.ChairpersonID).FirstOrDefault();

                    chairperson.ID = employee.ID;
                    chairperson.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    chairperson.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    chairperson.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(chairperson);
        }

        // Получение полного имени секретаря комиссии по id ГЭКа
        public ActionResult GetFullNameSecretary(int? id)
        {
            vEmployee secretary = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.SecretaryID).FirstOrDefault();

                    secretary.ID = employee.ID;
                    secretary.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    secretary.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    secretary.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(secretary);
        }

        // Получение фамилии и инициалов секретаря комиссии по id ГЭКа
        public ActionResult GetShortNameSecretaryOne(int? id)
        {
            vEmployee secretary = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.SecretaryID).FirstOrDefault();

                    secretary.ID = employee.ID;
                    secretary.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    secretary.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    secretary.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(secretary);
        }

        // Получение инициалов и фамилии секретаря комиссии по id ГЭКа
        public ActionResult GetShortNameSecretaryTwo(int? id)
        {
            vEmployee secretary = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.SecretaryID).FirstOrDefault();

                    secretary.ID = employee.ID;
                    secretary.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    secretary.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    secretary.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(secretary);
        }

    }
}