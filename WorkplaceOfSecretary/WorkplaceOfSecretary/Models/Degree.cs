using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    [Table("Degree")]
    public class Degree
    {
        public int ID { get; set; }
        public string NameOfDegree { get; set; }
        public string ShortName { get; set; }
    }
}