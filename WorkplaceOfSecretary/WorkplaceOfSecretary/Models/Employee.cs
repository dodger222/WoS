using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string LastNameInGenitive { get; set; }
        [Column("IdDegree")]
        public int DegreeID { get; set; }
        [Column("IdRank")]
        public int RankID { get; set; }

        public virtual Degree Degree { get; set; }
        public virtual Rank Rank { get; set; }

        //public virtual ICollection<Committee> CommitteesForChairperson { get; set; }
        //public virtual ICollection<Committee> CommitteesForSecretary { get; set; }
        //public virtual ICollection<Committee> CommitteesForMembers { get; set; }
        //public virtual ICollection<Protocol> Protocols { get; set; } 
    }
}