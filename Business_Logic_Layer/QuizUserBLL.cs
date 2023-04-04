using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;
using Data_Access_Layer.DAL;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Business_Logic_Layer
{
    public class QuizUserBLL : InterfaceQuizUser
    {

        private QuizUserDAL _DAL;
        private Mapper _QuizUserMapper;

        public QuizUserBLL()
        {
            _DAL = new Data_Access_Layer.DAL.QuizUserDAL();
            var _configQuizUser = new MapperConfiguration(cfg => cfg.CreateMap<QuizUser, QuizUserModel>().ReverseMap());

            _QuizUserMapper = new Mapper(_configQuizUser);
        }

        public List<QuizUserModel> GetAllQuizUser()
        {
            List<QuizUser> quizUserFromDB = _DAL.GetAllQuizUser();
            List<QuizUserModel> quizUserModel = _QuizUserMapper.Map<List<QuizUser>, List<QuizUserModel>>(quizUserFromDB);

            return quizUserModel;
        }

        public QuizUserModel GetQuizUserById(int id)
        {
            var quizUserEntity = _DAL.GetQuizUserById(id);

            QuizUserModel quizUserModel = _QuizUserMapper.Map<QuizUser, QuizUserModel>(quizUserEntity);

            return quizUserModel;
        }

        public ActionResult<IEnumerable<QuizUserModel>> GetQuizUserByLinkId(int id)
        {
            var quizUserEntity = _DAL.GetQuizUserByLinkId(id);
            var result = new List<QuizUserModel>();

            foreach (var item in quizUserEntity)
            {
                result.Add(_QuizUserMapper.Map<QuizUser, QuizUserModel>(item));
            }

            return result;
        }


        public QuizUserModel PostQuizUser(QuizUserModel quizUserModel)
        {
            QuizUser quizUserEntity = _QuizUserMapper.Map<QuizUserModel, QuizUser>(quizUserModel);
            var quizUser = _DAL.PostQuizUser(quizUserEntity);
            QuizUserModel quizUserModelReturn = _QuizUserMapper.Map<QuizUser, QuizUserModel>(quizUser);
            return quizUserModelReturn;
        }


        public QuizUserModel PatchQuizUser(int id, JsonPatchDocument<QuizUser> quizUserModelJSON)
        {
            var quizUserEntity = _DAL.PatchQuizUser(id, quizUserModelJSON);

            QuizUserModel quizUserModel = _QuizUserMapper.Map<QuizUser, QuizUserModel>(quizUserEntity);

            return quizUserModel;
        }

        public QuizUserModel PutQuizUser(QuizUserModel quizUserModel)
        {
            QuizUser quizUserEntity = _QuizUserMapper.Map<QuizUserModel, QuizUser>(quizUserModel);
            var quizUser = _DAL.PutQuizUser(quizUserEntity);
            QuizUserModel quizUserModelReturn = _QuizUserMapper.Map<QuizUser, QuizUserModel>(quizUser);
            return quizUserModelReturn;
        }
        public void DeleteQuizUser(int id)
        {
            _DAL.DeleteQuizUser(id);
        }
    }
}

