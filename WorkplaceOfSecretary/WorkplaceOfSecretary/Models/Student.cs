using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    [Table("Student")]
    public class Student
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        [Column("IdGroup")]
        public int GroupID { get; set; }
        public decimal AverageScore { get; set; }
        public bool Foreigner { get; set; }

        public Group Group { get; set; }
        //public ICollection<Protocol> Protocols { get; set; }
    }
}
