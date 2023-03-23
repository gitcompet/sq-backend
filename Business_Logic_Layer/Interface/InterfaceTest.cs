using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTest
    {

        List<TestModel> GetAllTest();
        TestModel GetTestById(int id);
        TestModel PostTest(TestModel testModel);
        TestModel PatchTest(int id, JsonPatchDocument<Test> testModelJSON);
        TestModel PutTest(TestModel testModel);
        void DeleteTest(int id);
    }
}
