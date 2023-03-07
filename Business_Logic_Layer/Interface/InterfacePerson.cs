using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfacePerson
    {

        List<PersonModel> GetAllPersons();


        PersonModel GetPersonById(int id);
        void PostPerson(PersonModel personModel);
    }
}
