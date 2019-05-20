using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    [Table("Specialty")]
    public class Specialty
    {
        public int ID { get; set; }
        public string NameOfSpecialty { get; set; }
        public string NumberOfSpecialty { get; set; }

        public virtual ICollection<Group> Groups { get; set; }
    }
}