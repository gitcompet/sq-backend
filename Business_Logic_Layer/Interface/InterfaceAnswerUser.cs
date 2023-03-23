using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceAnswerUser
    {

        List<AnswerUserModel> GetAllAnswerUser();
        AnswerUserModel GetAnswerUserById(int id);
        ActionResult<IEnumerable<AnswerUserModel>> GetAnswerUserByLinkId(int id);
        AnswerUserModel PostAnswerUser(AnswerUserModel answerUserModel);
        AnswerUserModel PatchAnswerUser(int id, JsonPatchDocument<AnswerUser> answerUserModelJSON);
        AnswerUserModel PutAnswerUser(AnswerUserModel answerUserModel);
        void DeleteAnswerUser(int id);
    }
}
