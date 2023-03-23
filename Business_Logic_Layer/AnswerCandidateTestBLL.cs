using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;

namespace Business_Logic_Layer
{
    public class AnswerCandidateTestBLL : InterfaceAnswerCandidateTest
    {

        private AnswerCandidateTestDAL _DAL;
        private Mapper _AnswerCandidateTestMapper;

        public AnswerCandidateTestBLL()
        {
            _DAL = new Data_Access_Layer.DAL.AnswerCandidateTestDAL();
            var _configAnswerCandidateTest = new MapperConfiguration(cfg => cfg.CreateMap<AnswerCandidateTest, AnswerCandidateTestModel>().ReverseMap());

            _AnswerCandidateTestMapper = new Mapper(_configAnswerCandidateTest);
        }

        public List<AnswerCandidateTestModel> GetAllAnswerCandidateTest()
        {
            List<AnswerCandidateTest> answercandidatetestFromDB = _DAL.GetAllAnswerCandidateTest();
            List<AnswerCandidateTestModel> answercandidatetestModel = _AnswerCandidateTestMapper.Map<List<AnswerCandidateTest>, List<AnswerCandidateTestModel>>(answercandidatetestFromDB);

            return answercandidatetestModel;
        }

        public AnswerCandidateTestModel GetAnswerCandidateTestById(int id)
        {
            var answercandidatetestEntity = _DAL.GetAnswerCandidateTestById(id);

            AnswerCandidateTestModel answercandidatetestModel = _AnswerCandidateTestMapper.Map<AnswerCandidateTest, AnswerCandidateTestModel>(answercandidatetestEntity);

            return answercandidatetestModel;
        }


        public void PostAnswerCandidateTest(AnswerCandidateTestModel answercandidatetestModel)
        {
            AnswerCandidateTest answercandidatetestEntity = _AnswerCandidateTestMapper.Map<AnswerCandidateTestModel, AnswerCandidateTest>(answercandidatetestModel);
            _DAL.postAnswerCandidateTest(answercandidatetestEntity);
        }

    }
}

