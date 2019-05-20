using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    [Table("Group")]
    public class Group
    {
        public int ID { get; set; }
        [Column("IdSpecialty")]
        public int SpecialtyID { get; set; }
        public string NumberOfGroup { get; set; }
        public string ComissionMembers { get; set; }

        public virtual Specialty Specialty { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}