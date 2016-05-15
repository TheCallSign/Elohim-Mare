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
            List<Student> students = new List<Student>();
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://ldap.ru.ac.za/ou=STUDENT,o=RU", "", "", AuthenticationTypes.Anonymous);
            DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
            {
                PageSize = int.MaxValue,
                Filter = "(&(objectClass=*))"
            };

            SearchResultCollection found = searcher.FindAll();

            foreach (SearchResult l in found)
            {
                //var result = l.Key;
                Student s = new Student();
                StringBuilder sb = new StringBuilder();
                foreach (DictionaryEntry p in l.Properties)
                {
                    //Console.WriteLine(String.Format("{0}:{1}", p.Key, ((ResultPropertyValueCollection)p.Value)[0]));
                    ResultPropertyValueCollection a = (ResultPropertyValueCollection)p.Value;
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
