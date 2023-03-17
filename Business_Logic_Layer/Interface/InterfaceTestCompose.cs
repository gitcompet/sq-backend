using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTestCompose
    {

        List<TestComposeModel> GetAllTestCompose();
        TestComposeModel GetTestComposeById(int id);
        TestComposeModel PostTestCompose(TestComposeModel testComposeModel);
        TestComposeModel PatchTestCompose(int id, JsonPatchDocument<TestCompose> testComposeModelJSON);
        TestComposeModel PutTestCompose(TestComposeModel testComposeModel);
        void DeleteTestCompose(int id);
    }
}
