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
                PageSize = int.MaxValue, //sets limit of entries to max integer value
                Filter = "(&(objectClass=*))" // at the moment finds all students in ou=Student directory without any filters
            };

            SearchResultCollection found = searcher.FindAll(); // The final list of all the found students in the ldap

            foreach (SearchResult l in found)
            {
                //var result = l.Key;
                Student s = new Student(); // creating a new student list
                StringBuilder sb = new StringBuilder();
                foreach (DictionaryEntry p in l.Properties)
                {
                    //Console.WriteLine(String.Format("{0}:{1}", p.Key, ((ResultPropertyValueCollection)p.Value)[0]));
                    ResultPropertyValueCollection a = (ResultPropertyValueCollection)p.Value;
                   // string value = "";
                    switch (p.Key.ToString())
                    {
                        case "uid":
                            s.studentNumber = ((ResultPropertyValueCollection)p.Value)[0].ToString();
                            break;
                        case "initials":
                            s.initials = ((ResultPropertyValueCollection)p.Value)[0].ToString();
                            break;
                        case "mail":
                            s.mail = ((ResultPropertyValueCollection)p.Value)[0].ToString();
                            break;
                        case "fullname":
                            s.fullName = ((ResultPropertyValueCollection)p.Value)[0].ToString();
                            break;
                        case "sn":
                            s.surname = ((ResultPropertyValueCollection)p.Value)[0].ToString();
                            break;
                        case "allowunlimitedcredit":
                            s.allowUnlimitedCredit = Convert.ToBoolean(((ResultPropertyValueCollection)p.Value)[0].ToString());
                            break;
                        case "preferredname":
                            s.preferredName = ((ResultPropertyValueCollection)p.Value)[0].ToString();
                            break;
                        case "loginexpirationtime":
                            s.loginExpiration = Convert.ToDateTime(((ResultPropertyValueCollection)p.Value)[0].ToString());
                            break;
                        case "logindisabled":
                            s.loginDisabled = Convert.ToBoolean(((ResultPropertyValueCollection)p.Value)[0].ToString());
                            break;
                        case "accesscardnumber":
                            s.accessCardNumber = Convert.ToInt32(((ResultPropertyValueCollection)p.Value)[0].ToString());
                            break;
                    }
                    for(int i = 0; i< a.Count; i++)
                    {
                        sb.Append(p.Key).Append(">").Append(a[i]).Append(":");
                    }
                    sb.Append("\n");
                }
                s.stuff = sb.ToString();

                students.Add(s);
            }

        
            return students;
        }
    }
}
