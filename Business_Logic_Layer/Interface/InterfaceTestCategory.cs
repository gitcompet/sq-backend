using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTestCategory
    {

        List<TestCategoryModel> GetAllTestCategory();
        TestCategoryModel GetTestCategoryById(int id);
        TestCategoryModel PostTestCategory(TestCategoryModel testCategoryModel);
        TestCategoryModel PatchTestCategory(int id, JsonPatchDocument<TestCategory> testCategoryModelJSON);
        TestCategoryModel PutTestCategory(TestCategoryModel testCategoryModel);
        void DeleteTestCategory(int id);
    }
}
