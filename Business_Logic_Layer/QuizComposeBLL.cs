using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;

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
            List<QuizCompose> quizcomposeFromDB = _DAL.GetAllQuizCompose();
            List<QuizComposeModel> quizcomposeModel = _QuizComposeMapper.Map<List<QuizCompose>, List<QuizComposeModel>>(quizcomposeFromDB);

            return quizcomposeModel;
        }

        public QuizComposeModel GetQuizComposeById(int id)
        {
            var quizcomposeEntity = _DAL.GetQuizComposeById(id);

            QuizComposeModel quizcomposeModel = _QuizComposeMapper.Map<QuizCompose, QuizComposeModel>(quizcomposeEntity);

            return quizcomposeModel;
        }


        public void PostQuizCompose(QuizComposeModel quizcomposeModel)
        {
            QuizCompose quizcomposeEntity = _QuizComposeMapper.Map<QuizComposeModel, QuizCompose>(quizcomposeModel);
            _DAL.postQuizCompose(quizcomposeEntity);
        }

    }
}