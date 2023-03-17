using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceQuiz
    {

        List<QuizModel> GetAllQuiz();
        QuizModel GetQuizById(int id);
        QuizModel PostQuiz(QuizModel quizModel);
        QuizModel PatchQuiz(int id, JsonPatchDocument<Quiz> quizModelJSON);
        QuizModel PutQuiz(QuizModel quizModel);
        void DeleteQuiz(int id);
    }
}
