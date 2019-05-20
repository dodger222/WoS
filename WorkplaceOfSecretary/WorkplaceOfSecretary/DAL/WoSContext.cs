﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WorkplaceOfSecretary.Models;

namespace WorkplaceOfSecretary.DAL
{
    public class WoSContext : DbContext
    {
        public WoSContext() : base("WoSContext")
        {
        }

        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}