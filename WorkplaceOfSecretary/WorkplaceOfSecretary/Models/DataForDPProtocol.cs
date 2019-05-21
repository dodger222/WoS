using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkplaceOfSecretary.Models
{
    public class DataForDPProtocol
    {
        public string StLastNameInGen { get; set; }
        public string StFirstNameInGen { get; set; }
        public string StPatronymicInGen { get; set; }
        public string Specialty { get; set; }
        public string Theme { get; set; }

        public string FullNameChairperson { get; set; }
        public string ShortNameChairpersonOne { get; set; }
        public string ShortNameChairpersonTwo { get; set; }

        public string FullNameSecretary { get; set; }
        public string ShortNameSecretaryOne { get; set; }
        public string ShortNameSecretaryTwo { get; set; }

        public string FullNameMemberOne { get; set; }
        public string ShortNameMemberOneOne { get; set; }
        public string ShortNameMemberOneTwo { get; set; }

        public string FullNameMemberTwo { get; set; }
        public string ShortNameMemberTwoOne { get; set; }
        public string ShortNameMemberTwoTwo { get; set; }

        public string FullNameMemberThree { get; set; }
        public string ShortNameMemberThreeOne { get; set; }
        public string ShortNameMemberThreeTwo { get; set; }

        public string FullNameLeader { get; set; }
        public string ShortNameLeaderInGen { get; set; }

    }
}