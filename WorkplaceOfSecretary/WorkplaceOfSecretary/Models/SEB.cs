using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    [Table("SEB")]
    public class SEB
    {
        public int ID { get; set; }
        public string NameOfSEB { get; set; }

        //public virtual ICollection<Meeting> Meetings { get; set; }
        //public virtual ICollection<Committee> Committees { get; set; }
    }
}