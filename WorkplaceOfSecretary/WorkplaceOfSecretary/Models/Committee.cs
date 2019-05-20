using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    [Table("Committee")]
    public class Committee
    {
        public int ID { get; set; }
        [Column("IdSEB")]
        public int SebID { get; set; }
        [Column("IdChairperson")]
        public int ChairpersonID { get; set; }
        [Column("IdSecretary")]
        public int SecretaryID { get; set; }
        [Column("IdMember1")]
        public int MemberOneID { get; set; }
        [Column("IdMember2")]
        public int MemberTwoID { get; set; }
        [Column("IdMember3")]
        public int MemberThreeID { get; set; }

        public virtual SEB Seb { get; set; }
        public virtual Employee Chairperson { get; set; }
        public virtual Employee Secretary { get; set; }
        public virtual Employee MemberOne { get; set; }
        public virtual Employee MemberTwo { get; set; }
        public virtual Employee MemberThree { get; set; }
    }
}
