using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WorkplaceOfSecretary.Models;

namespace WorkplaceOfSecretary.DAL
{
    public class WoSContext : DbContext
    {
        public WoSContext() : base("DefaultConnection")
        {
        }

        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SEB> SEBs { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Committee> Committees { get; set; }
    }
}