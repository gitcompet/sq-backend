using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceQuizUser
    {

        List<QuizUserModel> GetAllQuizUser();
        QuizUserModel GetQuizUserById(int id);
        ActionResult<IEnumerable<QuizUserModel>> GetQuizUserByLinkId(int id);
        QuizUserModel PostQuizUser(QuizUserModel quizUserModel);
        QuizUserModel PatchQuizUser(int id, JsonPatchDocument<QuizUser> quizUserModelJSON);
        QuizUserModel PutQuizUser(QuizUserModel quizUserModel);
        void DeleteQuizUser(int id);
        List<QuizUserModel> GetLastQuizValidates(int qty);
        List<QuizUserModel> GetPendingQuizes(int qty);
    }
}
