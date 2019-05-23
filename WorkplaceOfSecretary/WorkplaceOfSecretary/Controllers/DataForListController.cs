﻿using System;
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

            ViewBag.Sebs = new SelectList(sebs, "NameOfSEB", "NameOfSEB");

            List<Group> groups = new List<Group>();
            groups.Add(null);
            groups.AddRange(db.Groups.ToList());

            ViewBag.Groups = new SelectList(groups, "ID", "NumberOfGroup");

            return View();
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
    }
}