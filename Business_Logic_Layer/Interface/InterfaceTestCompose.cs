using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTestCompose
    {

        List<TestComposeModel> GetAllTestCompose();


        TestComposeModel GetTestComposeById(int id);
        void PostTestCompose(TestComposeModel testcomposeModel);
    }
}
