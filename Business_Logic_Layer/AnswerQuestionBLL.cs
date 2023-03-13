using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;

namespace Business_Logic_Layer
{
    public class AnswerQuestionBLL : InterfaceAnswerQuestion
    {

        private Data_Access_Layer.AnswerQuestionDAL _DAL;
        private Mapper _AnswerQuestionMapper;

        public AnswerQuestionBLL()
        {
            _DAL = new Data_Access_Layer.AnswerQuestionDAL();
            var _configAnswerQuestion = new MapperConfiguration(cfg => cfg.CreateMap<AnswerQuestion, AnswerQuestionModel>().ReverseMap());

            _AnswerQuestionMapper = new Mapper(_configAnswerQuestion);
        }

        public List<AnswerQuestionModel> GetAllAnswerQuestion()
        {
            List<AnswerQuestion> answerquestionFromDB = _DAL.GetAllAnswerQuestion();
            List<AnswerQuestionModel> answerquestionModel = _AnswerQuestionMapper.Map<List<AnswerQuestion>, List<AnswerQuestionModel>>(answerquestionFromDB);

            return answerquestionModel;
        }

        public AnswerQuestionModel GetAnswerQuestionById(int id)
        {
            var answerquestionEntity = _DAL.GetAnswerQuestionById(id);

            AnswerQuestionModel answerquestionModel = _AnswerQuestionMapper.Map<AnswerQuestion, AnswerQuestionModel>(answerquestionEntity);

            return answerquestionModel;
        }


        public void PostAnswerQuestion(AnswerQuestionModel answerquestionModel)
        {
            AnswerQuestion answerquestionEntity = _AnswerQuestionMapper.Map<AnswerQuestionModel, AnswerQuestion>(answerquestionModel);
            _DAL.postAnswerQuestion(answerquestionEntity);
        }

    }
}


