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
    public class QuestionBLL : InterfaceQuestion
    {

        private QuestionDAL _DAL;
        private Mapper _QuestionMapper;

        public QuestionBLL()
        {
            _DAL = new Data_Access_Layer.DAL.QuestionDAL();
            var _configQuestion = new MapperConfiguration(cfg => cfg.CreateMap<Question, QuestionModel>().ReverseMap());

            _QuestionMapper = new Mapper(_configQuestion);
        }

        public List<QuestionModel> GetAllQuestion()
        {
            List<Question> questionFromDB = _DAL.GetAllQuestion();
            List<QuestionModel> questionModel = _QuestionMapper.Map<List<Question>, List<QuestionModel>>(questionFromDB);

            return questionModel;
        }

        public QuestionModel GetQuestionById(int id)
        {
            var questionEntity = _DAL.GetQuestionById(id);

            QuestionModel questionModel = _QuestionMapper.Map<Question, QuestionModel>(questionEntity);

            return questionModel;
        }


        public QuestionModel PostQuestion(QuestionModel questionModel)
        {
            Question questionEntity = _QuestionMapper.Map<QuestionModel, Question>(questionModel);
            var question = _DAL.PostQuestion(questionEntity);
            QuestionModel questionModelReturn = _QuestionMapper.Map<Question, QuestionModel>(question);
            return questionModelReturn;
        }


        public QuestionModel PatchQuestion(int id, JsonPatchDocument<Question> questionModelJSON)
        {
            var questionEntity = _DAL.PatchQuestion(id, questionModelJSON);

            QuestionModel questionModel = _QuestionMapper.Map<Question, QuestionModel>(questionEntity);

            return questionModel;
        }

        public QuestionModel PutQuestion(QuestionModel questionModel)
        {
            Question questionEntity = _QuestionMapper.Map<QuestionModel, Question>(questionModel);
            var question = _DAL.PutQuestion(questionEntity);
            QuestionModel questionModelReturn = _QuestionMapper.Map<Question, QuestionModel>(question);
            return questionModelReturn;
        }
        public void DeleteQuestion(int id)
        {
            _DAL.DeleteQuestion(id);
        }
    }
}

