using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;
using Microsoft.AspNetCore.JsonPatch;

namespace Business_Logic_Layer
{
    public class AnswerBLL : InterfaceAnswer
    {

        private AnswerDAL _DAL;
        private Mapper _AnswerMapper;

        public AnswerBLL()
        {
            _DAL = new Data_Access_Layer.DAL.AnswerDAL();
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


        public AnswerModel PostAnswer(AnswerModel answerModel)
        {
            Answer answerEntity = _AnswerMapper.Map<AnswerModel, Answer>(answerModel);
            var answer = _DAL.PostAnswer(answerEntity);
            AnswerModel answerModelReturn = _AnswerMapper.Map<Answer, AnswerModel>(answer);
            return answerModelReturn;
        }


        public AnswerModel PatchAnswer(int id, JsonPatchDocument<Answer> answerModelJSON)
        {
            var answerEntity = _DAL.PatchAnswer(id, answerModelJSON);

            AnswerModel answerModel = _AnswerMapper.Map<Answer, AnswerModel>(answerEntity);

            return answerModel;
        }

        public AnswerModel PutAnswer(AnswerModel answerModel)
        {
            Answer answerEntity = _AnswerMapper.Map<AnswerModel, Answer>(answerModel);
            var answer = _DAL.PutAnswer(answerEntity);
            AnswerModel answerModelReturn = _AnswerMapper.Map<Answer, AnswerModel>(answer);
            return answerModelReturn;
        }
        public void DeleteAnswer(int id)
        {
            _DAL.DeleteAnswer(id);
        }
    }
}

