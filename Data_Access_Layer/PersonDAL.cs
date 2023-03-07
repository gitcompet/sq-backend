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
            var db = new PersonDbContext();
            return db.Person.ToList();
        }

        public Person GetPersonById(int id)
        {
            var db = new PersonDbContext();
            Person p = new Person();

            p = db.Person.FirstOrDefault(x => x.Id == id);      

            return p;
        }


        public void postPerson(Person person)
        {
            var db = new PersonDbContext();
            db.Add(person);
            db.SaveChanges();
        }

    }
}
