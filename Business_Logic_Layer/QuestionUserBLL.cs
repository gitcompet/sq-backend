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
    public class QuestionUserBLL : InterfaceQuestionUser
    {

        private QuestionUserDAL _DAL;
        private Mapper _QuestionUserMapper;

        public QuestionUserBLL()
        {
            _DAL = new Data_Access_Layer.DAL.QuestionUserDAL();
            var _configQuestionUser = new MapperConfiguration(cfg => cfg.CreateMap<QuestionUser, QuestionUserModel>().ReverseMap());

            _QuestionUserMapper = new Mapper(_configQuestionUser);
        }

        public List<QuestionUserModel> GetAllQuestionUser()
        {
            List<QuestionUser> questionUserFromDB = _DAL.GetAllQuestionUser();
            List<QuestionUserModel> questionUserModel = _QuestionUserMapper.Map<List<QuestionUser>, List<QuestionUserModel>>(questionUserFromDB);

            return questionUserModel;
        }

        public QuestionUserModel GetQuestionUserById(int id)
        {
            var questionUserEntity = _DAL.GetQuestionUserById(id);

            QuestionUserModel questionUserModel = _QuestionUserMapper.Map<QuestionUser, QuestionUserModel>(questionUserEntity);

            return questionUserModel;
        }

        public ActionResult<IEnumerable<QuestionUserModel>> GetQuestionUserByLinkId(int id)
        {
            var quizUserEntity = _DAL.GetQuestionUserByLinkId(id);
            var result = new List<QuestionUserModel>();

            foreach (var item in quizUserEntity)
            {
                result.Add(_QuestionUserMapper.Map<QuestionUser, QuestionUserModel>(item));
            }

            return result;
        }


        public QuestionUserModel PostQuestionUser(QuestionUserModel questionUserModel)
        {
            QuestionUser questionUserEntity = _QuestionUserMapper.Map<QuestionUserModel, QuestionUser>(questionUserModel);
            var questionUser = _DAL.PostQuestionUser(questionUserEntity);
            QuestionUserModel questionUserModelReturn = _QuestionUserMapper.Map<QuestionUser, QuestionUserModel>(questionUser);
            return questionUserModelReturn;
        }


        public QuestionUserModel PatchQuestionUser(int id, JsonPatchDocument<QuestionUser> questionUserModelJSON)
        {
            var questionUserEntity = _DAL.PatchQuestionUser(id, questionUserModelJSON);

            QuestionUserModel questionUserModel = _QuestionUserMapper.Map<QuestionUser, QuestionUserModel>(questionUserEntity);

            return questionUserModel;
        }
        public bool PatchQuestionUserHidden(int id, DateTime maxValidationDate)
        {
            return _DAL.PatchQuestionUserHidden(id, maxValidationDate);
        }

        public QuestionUserModel PutQuestionUser(QuestionUserModel questionUserModel)
        {
            QuestionUser questionUserEntity = _QuestionUserMapper.Map<QuestionUserModel, QuestionUser>(questionUserModel);
            var questionUser = _DAL.PutQuestionUser(questionUserEntity);
            QuestionUserModel questionUserModelReturn = _QuestionUserMapper.Map<QuestionUser, QuestionUserModel>(questionUser);
            return questionUserModelReturn;
        }
        public void DeleteQuestionUser(int id)
        {
            _DAL.DeleteQuestionUser(id);
        }
    }
}

