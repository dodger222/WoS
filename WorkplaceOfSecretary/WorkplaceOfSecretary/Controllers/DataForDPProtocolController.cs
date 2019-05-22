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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DataForDPProtocol dataForDPProtocol)
        {
            if (ModelState.IsValid)
            {
                Application app = new Application();
                Document doc = app.Documents.Add(@"F:\Protocol.docx");
                doc.Bookmarks["FIO"].Range.Text = dataForDPProtocol.StLastNameInGen + " " + dataForDPProtocol.StFirstNameInGen + " " + dataForDPProtocol.StPatronymicInGen;
                doc.Bookmarks["Specialty"].Range.Text = dataForDPProtocol.Specialty;
                doc.Bookmarks["Theme"].Range.Text = dataForDPProtocol.Theme;
                doc.SaveAs(FileName: @"F:\NewProtocolQQQ.docx");
                try
                {
                    doc.Close();
                    app.Quit();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                return RedirectToAction("Index", "Home");
            }
            
            return View(dataForDPProtocol);
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
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Student student = new Student();
                student = db.Students.Where(s => s.ID == id).FirstOrDefault();

                dataForDPProtocol.StLastNameInGen = student.LastName;
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение имени студента по id студента
        public ActionResult GetFirstNameStudent(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();
            
            if (id != null)
            {
                Student student = new Student();
                student = db.Students.Where(s => s.ID == id).FirstOrDefault();

                dataForDPProtocol.StFirstNameInGen = student.FirstName;
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение отчества студента по id студента
        public ActionResult GetPatronymicStudent(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();
            
            if (id != null)
            {
                Student student = new Student();
                student = db.Students.Where(s => s.ID == id).FirstOrDefault();

                dataForDPProtocol.StPatronymicInGen = student.Patronymic;
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение фамилии и инициалов студента в дательном падеже по id студента
        public ActionResult GetShortNameStudentInDat(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Student student = db.Students.Where(s => s.ID == id).FirstOrDefault();
                
                dataForDPProtocol.ShortNameStudentInDat = student.LastName + " " + student.FirstName.ToUpper()[0] + "." + student.Patronymic.ToUpper()[0] + ".";
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение номера и наименования специальности по id группы
        public ActionResult GetSpecialty(int? id)
        {

            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Group group = db.Groups.Where(g => g.ID == id).FirstOrDefault();

                Specialty specialty = db.Specialties.Where(s => s.ID == group.SpecialtyID).FirstOrDefault();

                dataForDPProtocol.Specialty = specialty.NumberOfSpecialty + " «" + specialty.NameOfSpecialty + "»";
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение полного имени председателя комиссии по id ГЭКа
        public ActionResult GetFullNameChairperson(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.ChairpersonID).FirstOrDefault();

                    dataForDPProtocol.FullNameChairperson = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение фамилии и инициалов председателя комиссии по id ГЭКа
        public ActionResult GetShortNameChairpersonOne(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.ChairpersonID).FirstOrDefault();

                    dataForDPProtocol.ShortNameChairpersonOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение инициалов и фамилии председателя комиссии по id ГЭКа
        public ActionResult GetShortNameChairpersonTwo(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.ChairpersonID).FirstOrDefault();

                    dataForDPProtocol.ShortNameChairpersonTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение полного имени секретаря комиссии по id ГЭКа
        public ActionResult GetFullNameSecretary(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.SecretaryID).FirstOrDefault();
                    
                    dataForDPProtocol.FullNameSecretary = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение фамилии и инициалов секретаря комиссии по id ГЭКа
        public ActionResult GetShortNameSecretaryOne(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.SecretaryID).FirstOrDefault();

                    dataForDPProtocol.ShortNameSecretaryOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение инициалов и фамилии секретаря комиссии по id ГЭКа
        public ActionResult GetShortNameSecretaryTwo(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.SecretaryID).FirstOrDefault();

                    dataForDPProtocol.ShortNameSecretaryTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение полного имени первого члена комиссии по id ГЭКа
        public ActionResult GetFullNameMemberOne(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberOneID).FirstOrDefault();

                    dataForDPProtocol.FullNameMemberOne = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение фамилии и инициалов первого члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberOneOne(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberOneID).FirstOrDefault();

                    dataForDPProtocol.ShortNameMemberOneOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение инициалов и фамилии первого члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberOneTwo(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberOneID).FirstOrDefault();

                    dataForDPProtocol.ShortNameMemberOneTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение полного имени второго члена комиссии по id ГЭКа
        public ActionResult GetFullNameMemberTwo(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberTwoID).FirstOrDefault();

                    dataForDPProtocol.FullNameMemberTwo = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение фамилии и инициалов второго члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberTwoOne(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberTwoID).FirstOrDefault();
                    
                    dataForDPProtocol.ShortNameMemberTwoOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение инициалов и фамилии второго члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberTwoTwo(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberTwoID).FirstOrDefault();

                    dataForDPProtocol.ShortNameMemberTwoTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение полного имени третьего члена комиссии по id ГЭКа
        public ActionResult GetFullNameMemberThree(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberThreeID).FirstOrDefault();

                    dataForDPProtocol.FullNameMemberThree = employee.LastName + " " + employee.FirstName + " " + employee.Patronymic;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение фамилии и инициалов третьего члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberThreeOne(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberThreeID).FirstOrDefault();

                    dataForDPProtocol.ShortNameMemberThreeOne = employee.LastName + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение инициалов и фамилии третьего члена комиссии по id ГЭКа
        public ActionResult GetShortNameMemberThreeTwo(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Committee committee = new Committee();
                committee = db.Committees.Where(c => c.SebID == id).FirstOrDefault();

                if (committee != null)
                {
                    Employee employee = new Employee();
                    employee = db.Employees.Where(e => e.ID == committee.MemberThreeID).FirstOrDefault();

                    dataForDPProtocol.ShortNameMemberThreeTwo = employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + "." + employee.LastName;
                }
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение фамилии и инициалов руководителя в родительном падеже по id работника
        public ActionResult GetShortNameLeaderInGen(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();

            if (id != null)
            {
                Employee employee = db.Employees.Where(e => e.ID == id).FirstOrDefault();

                dataForDPProtocol.ShortNameLeaderInGen = employee.LastNameInGenitive + " " + employee.FirstName.ToUpper()[0] + "." + employee.Patronymic.ToUpper()[0] + ".";
            }

            return PartialView(dataForDPProtocol);
        }

        // Получение консультантов по id группы
        public ActionResult GetConsultants(int? id)
        {
            DataForDPProtocol dataForDPProtocol = new DataForDPProtocol();
            
            if(id != null)
            {
                Group group = new Group();
                group = db.Groups.Where(g => g.ID == id).FirstOrDefault();

                dataForDPProtocol.Consultants = group.ComissionMembers;
            }

            return PartialView(dataForDPProtocol);
        }
    }
}