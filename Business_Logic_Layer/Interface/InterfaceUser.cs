using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceUser
    {

        List<UserModel> GetAllUser();
        UserModel GetUserById(int id);
        UserModel GetUserByUsername(string username);
        UserModel PostUser(UserModel userModel);
        UserModel PatchUser(int id, JsonPatchDocument<User> userModelJSON);
        UserModel PutUser(UserModel userModel);
        void DeleteUser(int id);
    }
}
