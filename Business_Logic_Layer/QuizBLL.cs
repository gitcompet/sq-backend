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
    public class QuizBLL : InterfaceQuiz
    {

        private QuizDAL _DAL;
        private Mapper _QuizMapper;

        public QuizBLL()
        {
            _DAL = new Data_Access_Layer.DAL.QuizDAL();
            var _configQuiz = new MapperConfiguration(cfg => cfg.CreateMap<Quiz, QuizModel>().ReverseMap());

            _QuizMapper = new Mapper(_configQuiz);
        }

        public List<QuizModel> GetAllQuiz()
        {
            List<Quiz> quizFromDB = _DAL.GetAllQuiz();
            List<QuizModel> quizModel = _QuizMapper.Map<List<Quiz>, List<QuizModel>>(quizFromDB);

            return quizModel;
        }

        public QuizModel GetQuizById(int id)
        {
            var quizEntity = _DAL.GetQuizById(id);

            QuizModel quizModel = _QuizMapper.Map<Quiz, QuizModel>(quizEntity);

            return quizModel;
        }


        public QuizModel PostQuiz(QuizModel quizModel)
        {
            Quiz quizEntity = _QuizMapper.Map<QuizModel, Quiz>(quizModel);
            var quiz = _DAL.PostQuiz(quizEntity);
            QuizModel quizModelReturn = _QuizMapper.Map<Quiz, QuizModel>(quiz);
            return quizModelReturn;
        }


        public QuizModel PatchQuiz(int id, JsonPatchDocument<Quiz> quizModelJSON)
        {
            var quizEntity = _DAL.PatchQuiz(id, quizModelJSON);

            QuizModel quizModel = _QuizMapper.Map<Quiz, QuizModel>(quizEntity);

            return quizModel;
        }

        public QuizModel PutQuiz(QuizModel quizModel)
        {
            Quiz quizEntity = _QuizMapper.Map<QuizModel, Quiz>(quizModel);
            var quiz = _DAL.PutQuiz(quizEntity);
            QuizModel quizModelReturn = _QuizMapper.Map<Quiz, QuizModel>(quiz);
            return quizModelReturn;
        }
        public void DeleteQuiz(int id)
        {
            _DAL.DeleteQuiz(id);
        }
    }
}

