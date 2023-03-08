using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer
{
    public class PersonDAL
    {
        public List<Person> GetAllPersons()
        {
            var db = new CompetenceDbContext();
            return db.Person.ToList();
        }

        public Person GetPersonById(int id)
        {
            var db = new CompetenceDbContext();
            Person p = new Person();

            p = db.Person.FirstOrDefault(x => x.Id == id);      

            return p;
        }


        public void postPerson(Person person)
        {
            var db = new CompetenceDbContext();
            db.Add(person);
            db.SaveChanges();
        }

    }
}
