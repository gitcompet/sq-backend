using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceAnswerCandidateTest
    {

        List<AnswerCandidateTestModel> GetAllAnswerCandidateTest();


        AnswerCandidateTestModel GetAnswerCandidateTestById(int id);
        void PostAnswerCandidateTest(AnswerCandidateTestModel answercandidatetestModel);
    }
}
