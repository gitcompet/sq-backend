using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceUserType
    {

        List<UserTypeModel> GetAllUserType();
        UserTypeModel GetUserTypeById(int id);
        UserTypeModel PostUserType(UserTypeModel userTypeModel);
        UserTypeModel PatchUserType(int id, JsonPatchDocument<UserType> userTypeModelJSON);
        UserTypeModel PutUserType(UserTypeModel userTypeModel);
        void DeleteUserType(int id);
    }
}
