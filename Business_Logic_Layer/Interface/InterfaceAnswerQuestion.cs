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
            void PostAnswerQuestion(AnswerQuestionModel answerquestionModel);
        }
    }
