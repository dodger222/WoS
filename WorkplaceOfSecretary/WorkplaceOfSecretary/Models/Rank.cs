using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    [Table("Rank")]
    public class Rank
    {
        public int ID { get; set; }
        public string NameOfRank { get; set; }
        public string NameInGenitive { get; set; }
    }
}