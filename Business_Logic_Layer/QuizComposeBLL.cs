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
    public class QuizComposeBLL : InterfaceQuizCompose
    {

        private QuizComposeDAL _DAL;
        private Mapper _QuizComposeMapper;

        public QuizComposeBLL()
        {
            _DAL = new Data_Access_Layer.DAL.QuizComposeDAL();
            var _configQuizCompose = new MapperConfiguration(cfg => cfg.CreateMap<QuizCompose, QuizComposeModel>().ReverseMap());

            _QuizComposeMapper = new Mapper(_configQuizCompose);
        }

        public List<QuizComposeModel> GetAllQuizCompose()
        {
            List<QuizCompose> quizComposeFromDB = _DAL.GetAllQuizCompose();
            List<QuizComposeModel> quizComposeModel = _QuizComposeMapper.Map<List<QuizCompose>, List<QuizComposeModel>>(quizComposeFromDB);

            return quizComposeModel;
        }

        public QuizComposeModel GetQuizComposeById(int id)
        {
            var quizComposeEntity = _DAL.GetQuizComposeById(id);

            QuizComposeModel quizComposeModel = _QuizComposeMapper.Map<QuizCompose, QuizComposeModel>(quizComposeEntity);

            return quizComposeModel;
        }


        public QuizComposeModel PostQuizCompose(QuizComposeModel quizComposeModel)
        {
            QuizCompose quizComposeEntity = _QuizComposeMapper.Map<QuizComposeModel, QuizCompose>(quizComposeModel);
            var quizCompose = _DAL.PostQuizCompose(quizComposeEntity);
            QuizComposeModel quizComposeModelReturn = _QuizComposeMapper.Map<QuizCompose, QuizComposeModel>(quizCompose);
            return quizComposeModelReturn;
        }


        public QuizComposeModel PatchQuizCompose(int id, JsonPatchDocument<QuizCompose> quizComposeModelJSON)
        {
            var quizComposeEntity = _DAL.PatchQuizCompose(id, quizComposeModelJSON);

            QuizComposeModel quizComposeModel = _QuizComposeMapper.Map<QuizCompose, QuizComposeModel>(quizComposeEntity);

            return quizComposeModel;
        }

        public QuizComposeModel PutQuizCompose(QuizComposeModel quizComposeModel)
        {
            QuizCompose quizComposeEntity = _QuizComposeMapper.Map<QuizComposeModel, QuizCompose>(quizComposeModel);
            var quizCompose = _DAL.PutQuizCompose(quizComposeEntity);
            QuizComposeModel quizComposeModelReturn = _QuizComposeMapper.Map<QuizCompose, QuizComposeModel>(quizCompose);
            return quizComposeModelReturn;
        }
        public void DeleteQuizCompose(int id)
        {
            _DAL.DeleteQuizCompose(id);
        }
    }
}

