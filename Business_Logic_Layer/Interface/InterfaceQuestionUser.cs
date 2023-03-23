using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceQuestionUser
    {

        List<QuestionUserModel> GetAllQuestionUser();
        QuestionUserModel GetQuestionUserById(int id);
        QuestionUserModel PostQuestionUser(QuestionUserModel questionUserModel);
        QuestionUserModel PatchQuestionUser(int id, JsonPatchDocument<QuestionUser> questionUserModelJSON);
        QuestionUserModel PutQuestionUser(QuestionUserModel questionUserModel);
        void DeleteQuestionUser(int id);
    }
}
