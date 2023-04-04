using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceAnswerQuestion
    {

        List<AnswerQuestionModel> GetAllAnswerQuestion();
        AnswerQuestionModel GetAnswerQuestionById(int id);
        IEnumerable<AnswerQuestionModelGet> GetAnswerQuestionByQuestionId(int id);
        AnswerQuestionModel PostAnswerQuestion(AnswerQuestionModel answerQuestionModel);
        AnswerQuestionModel PatchAnswerQuestion(int id, JsonPatchDocument<AnswerQuestion> answerQuestionModelJSON);
        AnswerQuestionModel PutAnswerQuestion(AnswerQuestionModel answerQuestionModel);
        void DeleteAnswerQuestion(int id);
    }
}
