using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare.EMBackend
{
    class LDAPManager
    {
        private LdapConnection connection;

        public LDAPManager(string url = "ldap.ru.ac.za")
        {
            connection = new LdapConnection(url);
        }
        public List<Student> LoadAllStudents()
        {
            List<Student> students = new List<Student>(); // new list of students gets populated from Ldap
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://ldap.ru.ac.za/ou=STUDENT,o=RU", "", "", AuthenticationTypes.Anonymous); // directory of where are we looking in the ldap, logging in anonymously
            DirectorySearcher searcher = new DirectorySearcher(directoryEntry) // search through the directory
            {
                PageSize = 1000, //sets limit of entries at a time
                Filter = "(&(objectClass=*))" // at the moment finds all students in ou=Student directory without any filters
            };

            searcher.Asynchronous = true;
            SearchResultCollection found = searcher.FindAll(); // The final list of all the found students in the ldap

            foreach (SearchResult l in found)
            {
                //var result = l.Key;
                Student s = new Student(); // creating a new student list
                foreach (DictionaryEntry p in l.Properties)
                {

                    var val = ((ResultPropertyValueCollection)p.Value)[0].ToString();
                    switch (p.Key.ToString())
                    {
                        case "uid":
                            s.studentNumber = val;
                            break;
                        case "initials":
                            s.initials = val;
                            break;
                        case "mail":
                            s.mail = val;
                            break;
                        case "fullname":
                            s.fullname = val;
                            break;
                        case "sn":
                            s.surname = val;
                            break;
                        case "allowunlimitedcredit":
                            s.allowUnlimitedCredit = Convert.ToBoolean(val);
                            break;
                        case "loginexpirationtime":
                            s.loginExpiration = Convert.ToDateTime(val);
                            break;
                        case "logindisabled":
                            s.loginDisabled = Convert.ToBoolean(val);
                            break;
                        case "accesscardnumber":
                            s.accessCardNumber = Convert.ToInt32(val);
                            break;
                    }
                }
                string _a = (new StringBuilder()).Append("https://scifac.ru.ac.za/timetable/personal/timetables/").Append(s.studentNumber).Append(".htm").ToString();
                s.timeTable = _a.ToString();
                students.Add(s);
            }
            searcher.Dispose();
            directoryEntry.Dispose();
            return students;
        }

        public List<Staff> LoadAllStaff()
        {
            List<Staff> staff = new List<Staff>();
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://ldap.ru.ac.za/ou=STAFF,o=RU", "", "", AuthenticationTypes.Anonymous); // directory of where are we looking in the ldap, logging in anonymously
            DirectorySearcher searcher = new DirectorySearcher(directoryEntry) { PageSize = 500, Filter = "(&(objectClass=*))" }; // search through the directory
            searcher.Asynchronous = true;
            SearchResultCollection found = searcher.FindAll(); // The final list of all the found staff in the ldap

            foreach (SearchResult l in found)
            {
                Staff s = new Staff(); // creating a new staff list
                foreach (DictionaryEntry p in l.Properties)
                {

                    var val = ((ResultPropertyValueCollection)p.Value)[0].ToString();

                    switch (p.Key.ToString())
                    {
                        case "uid":
                            s.staffUID = val;
                            break;
                        case "initials":
                            s.initials = val;
                            break;
                        case "mail":
                            s.mail = val;
                            break;
                        case "fullname":
                            s.fullname = val;
                            break;
                        case "sn":
                            s.surname = val;
                            break;
                        case "ou":
                            s.department = val;
                            break;
                        case "title":
                            s.title = val;
                            break;
                        case "telephonenumber":
                            s.telNumber = val;
                            break;
                        case "allowunlimitedcredit":
                            s.allowUnlimitedCredit = Convert.ToBoolean(val);
                            break;
                        case "loginexpirationtime":
                            s.loginExpiration = Convert.ToDateTime(val);
                            break;
                        case "logindisabled":
                            s.loginDisabled = Convert.ToBoolean(val);
                            break;
                        case "accesscardnumber":
                            s.accessCardNumber = Convert.ToInt32(val);
                            break;
                    }
                }
                staff.Add(s);
            }
            searcher.Dispose();
            directoryEntry.Dispose();
            return staff;
        }
    }
}
