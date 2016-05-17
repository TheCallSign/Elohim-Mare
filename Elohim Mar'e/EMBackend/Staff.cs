using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare.EMBackend
{
    class Staff
    {
        public string fullname { get; set; } = "";
        public string firstnames { get; set; } = "";
        public string surname { get; set; } = "";
        public string staffNumber { get; set; } = ""; // pretty much the staff number but with the 'g' prefix.
        public string mail { get; set; } = "";
        public string initials { get; set; } = "";
        public string homeDirectory { get; set; } = "";
        public string title { get; set; } = "";
        public string department { get; set; } = "";
        public int telNumber { get; set; }
        public DateTime loginExpiration { get; set; }
        public bool loginDisabled { get; set; }
        public int accessCardNumber { get; set; }
        public bool allowUnlimitedCredit { get; set; } //idk what this is
        public string LDAPPath { get; set; } // direct path to staff in LDAP
        override public string ToString()
        {
            return String.Format("{0}: {1}, {2}, {3}, {4}, {5}, {6}", fullname, staffNumber, mail, accessCardNumber, loginDisabled, title, department);
        }
    }
}
