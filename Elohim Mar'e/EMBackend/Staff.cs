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
        public string staffUID { get; set; } = "";
        public string mail { get; set; } = "";
        public string initials { get; set; } = "";
        public string homeDirectory { get; set; } = "";
        public string title { get; set; } = "";
        public string department { get; set; } = "";
        public string telNumber { get; set; } = "";
        public DateTime loginExpiration { get; set; }
        public bool loginDisabled { get; set; }
        public int accessCardNumber { get; set; }
        public bool allowUnlimitedCredit { get; set; } 
        override public string ToString()
        {
            return String.Format("Fullname : {0} \nStaffUID: {1} \nMail: {2} \nAccessCardNumber: {3} \nLoginDisabled: {4} \nTitle: {5} \nDepartment: {6} \nTelNumber: {7} ", fullname, staffUID, mail, accessCardNumber, loginDisabled, title, department, telNumber);
        }
    }
}
