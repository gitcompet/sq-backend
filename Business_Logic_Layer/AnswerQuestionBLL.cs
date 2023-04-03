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
    public class AnswerQuestionBLL : InterfaceAnswerQuestion
    {

        private AnswerQuestionDAL _DAL;
        private Mapper _AnswerQuestionMapper;

        public AnswerQuestionBLL()
        {
            _DAL = new Data_Access_Layer.DAL.AnswerQuestionDAL();
            var _configAnswerQuestion = new MapperConfiguration(cfg => cfg.CreateMap<AnswerQuestion, AnswerQuestionModel>().ReverseMap());

            _AnswerQuestionMapper = new Mapper(_configAnswerQuestion);
        }

        public List<AnswerQuestionModel> GetAllAnswerQuestion()
        {
            List<AnswerQuestion> answerQuestionFromDB = _DAL.GetAllAnswerQuestion();
            List<AnswerQuestionModel> answerQuestionModel = _AnswerQuestionMapper.Map<List<AnswerQuestion>, List<AnswerQuestionModel>>(answerQuestionFromDB);

            return answerQuestionModel;
        }

        public AnswerQuestionModel GetAnswerQuestionById(int id)
        {
            var answerQuestionEntity = _DAL.GetAnswerQuestionById(id);

            AnswerQuestionModel answerQuestionModel = _AnswerQuestionMapper.Map<AnswerQuestion, AnswerQuestionModel>(answerQuestionEntity);

            return answerQuestionModel;
        }
        public IEnumerable<AnswerQuestionModelGet> GetAnswerQuestionByQuestionId(int id)
        {
            var answerQuestionEntity = _DAL.GetAnswerQuestionByQuestionId(id);
            var result = new List<AnswerQuestionModelGet>();

            foreach (var item in answerQuestionEntity)
            {
                var temp = _AnswerQuestionMapper.Map<AnswerQuestion, AnswerQuestionModel>(item);
                result.Add(new AnswerQuestionModelGet(temp));
            }

            return result;
        }


        public AnswerQuestionModel PostAnswerQuestion(AnswerQuestionModel answerQuestionModel)
        {
            AnswerQuestion answerQuestionEntity = _AnswerQuestionMapper.Map<AnswerQuestionModel, AnswerQuestion>(answerQuestionModel);
            var answerQuestion = _DAL.PostAnswerQuestion(answerQuestionEntity);
            AnswerQuestionModel answerQuestionModelReturn = _AnswerQuestionMapper.Map<AnswerQuestion, AnswerQuestionModel>(answerQuestion);
            return answerQuestionModelReturn;
        }


        public AnswerQuestionModel PatchAnswerQuestion(int id, JsonPatchDocument<AnswerQuestion> answerQuestionModelJSON)
        {
            var answerQuestionEntity = _DAL.PatchAnswerQuestion(id, answerQuestionModelJSON);

            AnswerQuestionModel answerQuestionModel = _AnswerQuestionMapper.Map<AnswerQuestion, AnswerQuestionModel>(answerQuestionEntity);

            return answerQuestionModel;
        }

        public AnswerQuestionModel PutAnswerQuestion(AnswerQuestionModel answerQuestionModel)
        {
            AnswerQuestion answerQuestionEntity = _AnswerQuestionMapper.Map<AnswerQuestionModel, AnswerQuestion>(answerQuestionModel);
            var answerQuestion = _DAL.PutAnswerQuestion(answerQuestionEntity);
            AnswerQuestionModel answerQuestionModelReturn = _AnswerQuestionMapper.Map<AnswerQuestion, AnswerQuestionModel>(answerQuestion);
            return answerQuestionModelReturn;
        }
        public void DeleteAnswerQuestion(int id)
        {
            _DAL.DeleteAnswerQuestion(id);
        }
    }
}

