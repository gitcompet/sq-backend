using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTestCategoryCompose
    {

        List<TestCategoryComposeModel> GetAllTestCategoryCompose();
        TestCategoryComposeModel GetTestCategoryComposeById(int id);
        TestCategoryComposeModel PostTestCategoryCompose(TestCategoryComposeModel testCategoryComposeModel);
        TestCategoryComposeModel PatchTestCategoryCompose(int id, JsonPatchDocument<TestCategoryCompose> testCategoryComposeModelJSON);
        TestCategoryComposeModel PutTestCategoryCompose(TestCategoryComposeModel testCategoryComposeModel);
        IEnumerable<TestCategoryComposeModel> GetTestCategoryComposeByTestId(int id);
        void DeleteTestCategoryCompose(int id);
    }
}
