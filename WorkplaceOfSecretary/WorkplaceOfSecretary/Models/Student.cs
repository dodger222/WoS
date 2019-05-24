using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Range(typeof(decimal), "4,0", "10,0", ErrorMessage = "Наименьшая оценка - 4, максимальная - 10, в качестве разделителя дробной и целой части используется запятая")]
        public decimal AverageScore { get; set; }
        public bool Foreigner { get; set; }

        public virtual Group Group { get; set; }
        //public ICollection<Protocol> Protocols { get; set; }
    }
}
