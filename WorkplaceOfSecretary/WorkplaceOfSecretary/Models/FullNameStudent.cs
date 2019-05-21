using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    public class FullNameStudent
    {
        public int ID { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }

        public string FullName { get; set; }
    }
}