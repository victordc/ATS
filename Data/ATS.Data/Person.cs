using ATS.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATS.Data.Model
{
    public partial class Person
    {

        public static Person GetById(int PersonId)
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.Persons.Find(PersonId);
        }



        public static IEnumerable<Person> GetAll()
        {
            ATSCEEntities context = new ATSCEEntities();
            return context.Persons;
        }

    }
}
