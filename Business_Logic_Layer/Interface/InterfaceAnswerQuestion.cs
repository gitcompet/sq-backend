using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Interface
{
    public interface InterfaceAnswerQuestion
    {
            List<AnswerQuestionModel> GetAllAnswerQuestion();


        AnswerQuestionModel GetAnswerQuestionById(int id);
        Boolean[] GetAnswerQuestionByQuestionId(int id);
        IEnumerable<string> GetAnswerByListId(int id);
        void PostAnswerQuestion(AnswerQuestionModel answerquestionModel);
        }
    }
