using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElohimMare.Server
{
    class LDAPManager
    {
        private LdapConnection connection;

        public LDAPManager(string url = "ldap.ru.ac.za")
        {
            connection = new LdapConnection(url);
        }
        public string SearchForEntry(string entry)
        {
            //var request = new SearchRequest("ou=STUDENTS,dc=ldap,dc=ru,dc=ac,dc=za", "(objectClass=simpleSecurityObject)", SearchScope.Subtree, null);
            //var response = (SearchResponse)connection.SendRequest(request);
            //foreach (SearchResultEntry e in response.Entries)
            //{
            //    Console.WriteLine(e.ToString());
            //    //Process the entries
            //}
            
            DirectoryEntry directoryEntry = new DirectoryEntry("LDAP://ldap.ru.ac.za/ou=STUDENT,o=RU", "", "", AuthenticationTypes.Anonymous);
            //directoryEntry.Path = "LDAP://ldap.ru.ac.za/OU=STUDENT,O=RU,dc=ldap,dc=ru,dc=ac,dc=za";
            //directoryEntry.Path = "LDAP://ldap.ru.ac.za/OU=STUDENT,O=RU,dc=ldap,dc=ru";
            DirectorySearcher searcher = new DirectorySearcher(directoryEntry)
            {
                PageSize = int.MaxValue,
                Filter = "(&(objectClass=*))"
            };

            //searcher.PropertiesToLoad.Add("sn");
            //var r = directoryEntry.NativeGuid;
            //Console.WriteLine(r);
            //Console.ReadKey();
            //var result = searcher.FindAll();
            SearchResultCollection found = searcher.FindAll();
            foreach (SearchResult l in found)
            {
                //var result = l.Key;

                foreach (DictionaryEntry p in l.Properties)
                {

                    Console.WriteLine(String.Format("{0}:{1}", p.Key, ((ResultPropertyValueCollection)p.Value)[0]));

                }
                Console.WriteLine("");
            }

           
            //string surname;
            //
            //if (result.Properties.Contains("sn"))
            //{
            //    surname = result.Properties["sn"][0].ToString();
            //}
            return "";
        }
    }
}
