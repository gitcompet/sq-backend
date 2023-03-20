using Business_Logic_Layer.Models;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceAnswer
    {

        List<AnswerModel> GetAllAnswer();
        AnswerModel GetAnswerById(int id);
        void PostAnswer(int id);
        AnswerModel PatchAnswer(int id, JsonPatchDocument<Answer> answerModelJSON);
        AnswerModel PutAnswer(AnswerModel answerModel);
        void DeleteAnswer(int id);
    }
}
