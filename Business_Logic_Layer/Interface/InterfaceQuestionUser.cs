using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceQuestionUser
    {

        List<QuestionUserModel> GetAllQuestionUser();
        QuestionUserModel GetQuestionUserById(int id);
        ActionResult<IEnumerable<QuestionUserModel>> GetQuestionUserByLinkId(int id);
        QuestionUserModel PostQuestionUser(QuestionUserModel questionUserModel);
        QuestionUserModel PatchQuestionUser(int id, JsonPatchDocument<QuestionUser> questionUserModelJSON);
        bool PatchQuestionUserHidden(int id, DateTime maxValidationDate);
        QuestionUserModel PutQuestionUser(QuestionUserModel questionUserModel);
        void DeleteQuestionUser(int id);
    }
}
