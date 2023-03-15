using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTest
    {

        List<TestModel> GetAllTest();


        TestModel GetTestById(int id);
        void PostTest(TestModel testModel);
    }
}
