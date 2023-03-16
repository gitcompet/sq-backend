using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceQuestion
    {

        List<QuestionModel> GetAllQuestion();
        QuestionModel GetQuestionById(int id);
        QuestionModel PostQuestion(QuestionModel questionModel);
        QuestionModel PatchQuestion(int id, JsonPatchDocument<Question> questionModelJSON);
        QuestionModel PutQuestion(QuestionModel questionModel);
        void DeleteQuestion(int id);
    }
}
