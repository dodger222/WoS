using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    public class DataForList
    {
        public string SEB { get; set; }
        public DateTime Date { get; set; }
        public string Group { get; set; }

        public List<Student> Students { get; set; }
    }
}