using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTestUser
    {

        List<TestUserModel> GetAllTestUser();
        TestUserModel GetTestUserById(int id, bool isPending);
        IEnumerable<TestUserModel> GetTestUserByUserId(int id, bool isPending);
        TestUserModel PostTestUser(TestUserModel testUserModel);
        TestUserModel PatchTestUser(int id, JsonPatchDocument<TestUser> testUserModelJSON);
        TestUserModel PutTestUser(TestUserModel testUserModel);
        void DeleteTestUser(int id);
    }
}
