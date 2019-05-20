using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    [Table("Meeting")]
    public class Meeting
    {
        public int ID { get; set; }
        [Column("IdSEB")]
        public int SebID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public virtual SEB Seb { get; set; }
        //public virtual ICollection<Protocol> Protocols { get; set; }
    }
}