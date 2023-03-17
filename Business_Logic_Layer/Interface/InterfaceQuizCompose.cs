using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceQuizCompose
    {

        List<QuizComposeModel> GetAllQuizCompose();
        QuizComposeModel GetQuizComposeById(int id);
        QuizComposeModel PostQuizCompose(QuizComposeModel quizComposeModel);
        QuizComposeModel PatchQuizCompose(int id, JsonPatchDocument<QuizCompose> quizComposeModelJSON);
        QuizComposeModel PutQuizCompose(QuizComposeModel quizComposeModel);
        void DeleteQuizCompose(int id);
    }
}
