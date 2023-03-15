using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceQuizCompose
    {

        List<QuizComposeModel> GetAllQuizCompose();


        QuizComposeModel GetQuizComposeById(int id);
        void PostQuizCompose(QuizComposeModel quizcomposeModel);
    }
}
