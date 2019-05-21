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

            List<Leader> leaders = new List<Leader>();
            leaders.Add(null);

            foreach(var employee in db.Employees)
            {
                Leader leader = new Leader();
                leader.ID = employee.ID;
                leader.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                leader.ShortNameInGen = employee.LastNameInGenitive + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";

                leaders.Add(leader);
            }

            ViewBag.LeaderID = new SelectList(leaders, "ID", "FullName");

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

        // Получение полного имени первого члена комиссии по id ГЭКа
        public ActionResult GetFullNameMemberOne(int? id)
        {
            vEmployee memberOne = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberOneID).FirstOrDefault();

                    memberOne.ID = employee.ID;
                    memberOne.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    memberOne.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    memberOne.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(memberOne);
        }

        // Получение фамилии и инициалов первого члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberOneOne(int? id)
        {
            vEmployee memberOne = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberOneID).FirstOrDefault();

                    memberOne.ID = employee.ID;
                    memberOne.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    memberOne.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    memberOne.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(memberOne);
        }

        // Получение инициалов и фамилии первого члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberOneTwo(int? id)
        {
            vEmployee memberOne = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberOneID).FirstOrDefault();

                    memberOne.ID = employee.ID;
                    memberOne.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    memberOne.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    memberOne.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(memberOne);
        }

        // Получение полного имени второго члена комиссии по id ГЭКа
        public ActionResult GetFullNameMemberTwo(int? id)
        {
            vEmployee memberTwo = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberTwoID).FirstOrDefault();

                    memberTwo.ID = employee.ID;
                    memberTwo.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    memberTwo.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    memberTwo.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(memberTwo);
        }

        // Получение фамилии и инициалов второго члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberTwoOne(int? id)
        {
            vEmployee memberTwo = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberTwoID).FirstOrDefault();

                    memberTwo.ID = employee.ID;
                    memberTwo.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    memberTwo.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    memberTwo.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(memberTwo);
        }

        // Получение инициалов и фамилии второго члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberTwoTwo(int? id)
        {
            vEmployee memberTwo = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberTwoID).FirstOrDefault();

                    memberTwo.ID = employee.ID;
                    memberTwo.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    memberTwo.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    memberTwo.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(memberTwo);
        }

        // Получение полного имени третьего члена комиссии по id ГЭКа
        public ActionResult GetFullNameMemberThree(int? id)
        {
            vEmployee memberThree = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberThreeID).FirstOrDefault();

                    memberThree.ID = employee.ID;
                    memberThree.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    memberThree.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    memberThree.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(memberThree);
        }

        // Получение фамилии и инициалов третьего члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberThreeOne(int? id)
        {
            vEmployee memberThree = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberThreeID).FirstOrDefault();

                    memberThree.ID = employee.ID;
                    memberThree.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    memberThree.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    memberThree.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(memberThree);
        }

        // Получение инициалов и фамилии третьего члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberThreeTwo(int? id)
        {
            vEmployee memberThree = new vEmployee();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberThreeID).FirstOrDefault();

                    memberThree.ID = employee.ID;
                    memberThree.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                    memberThree.ShortFullNameOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                    memberThree.ShortFullNameTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(memberThree);
        }

        // Получение фамилии и инициалов руководителя в родительном падеже по id работника
        public ActionResult GetShortNameLeaderInGen(int? id)
        {
            Leader leader = new Leader();

            if (id != null)
            {
                Employee employee = db.Employees.Where(e => e.ID == id).FirstOrDefault();

                leader.ID = employee.ID;
                leader.FullName = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                leader.ShortNameInGen = employee.LastNameInGenitive + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
            }

            return PartialView(leader);
        }

    }
}