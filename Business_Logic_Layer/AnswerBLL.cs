using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;

namespace Business_Logic_Layer
{
    public class AnswerBLL : InterfaceAnswer
    {

        private Data_Access_Layer.AnswerDAL _DAL;
        private Mapper _AnswerMapper;

        public AnswerBLL()
        {
            _DAL = new Data_Access_Layer.AnswerDAL();
            var _configAnswer = new MapperConfiguration(cfg => cfg.CreateMap<Answer, AnswerModel>().ReverseMap());

            _AnswerMapper = new Mapper(_configAnswer);
        }

        public List<AnswerModel> GetAllAnswer()
        {
            List<Answer> answerFromDB = _DAL.GetAllAnswer();
            List<AnswerModel> answerModel = _AnswerMapper.Map<List<Answer>, List<AnswerModel>>(answerFromDB);

            return answerModel;
        }

        public AnswerModel GetAnswerById(int id)
        {
            var answerEntity = _DAL.GetAnswerById(id);

            AnswerModel answerModel = _AnswerMapper.Map<Answer, AnswerModel>(answerEntity);

            return answerModel;
        }


        public void PostAnswer(AnswerModel answerModel)
        {
            Answer answerEntity = _AnswerMapper.Map<AnswerModel, Answer>(answerModel);
            _DAL.postAnswer(answerEntity);
        }

    }
}


