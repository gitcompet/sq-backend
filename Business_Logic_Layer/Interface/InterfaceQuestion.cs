using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceQuestion
    {

        List<QuestionModel> GetAllQuestion();


        QuestionModel GetQuestionById(int id);
        void PostQuestion(QuestionModel questionModel);
    }
}
