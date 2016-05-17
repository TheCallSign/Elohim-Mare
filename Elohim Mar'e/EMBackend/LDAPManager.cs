﻿using System;
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
                PageSize = 500, //sets limit of entries to max integer value at a time
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
                }
<<<<<<< HEAD
                string _a = (new StringBuilder()).Append("https://scifac.ru.ac.za/timetable/personal/timetables/").Append(s.studentNumber).Append(".htm").ToString();
                s.timeTable = _a.ToString();
                students.Add(s);              
=======
                string _a = (new StringBuilder()).Append("https://scifac.ru.ac.za/timetable/personal/timetables/").Append(s.studentNumber.ToUpper()).Append(".htm").ToString();
                s.timeTable = _a.ToString();
                s.stuff = sb.ToString();
                students.Add(s);
>>>>>>> fd8c6ba776d20d3585924802cf74d905642f38d6
            }
            searcher.Dispose();
            directoryEntry.Dispose();
            return students;
        }
    }
}
