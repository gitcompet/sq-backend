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
    public class AnswerUserBLL : InterfaceAnswerUser
    {

        private AnswerUserDAL _DAL;
        private Mapper _AnswerUserMapper;

        public AnswerUserBLL()
        {
            _DAL = new Data_Access_Layer.DAL.AnswerUserDAL();
            var _configAnswerUser = new MapperConfiguration(cfg => cfg.CreateMap<AnswerUser, AnswerUserModel>().ReverseMap());

            _AnswerUserMapper = new Mapper(_configAnswerUser);
        }

        public List<AnswerUserModel> GetAllAnswerUser()
        {
            List<AnswerUser> answerUserFromDB = _DAL.GetAllAnswerUser();
            List<AnswerUserModel> answerUserModel = _AnswerUserMapper.Map<List<AnswerUser>, List<AnswerUserModel>>(answerUserFromDB);

            return answerUserModel;
        }

        public AnswerUserModel GetAnswerUserById(int id)
        {
            var answerUserEntity = _DAL.GetAnswerUserById(id);

            AnswerUserModel answerUserModel = _AnswerUserMapper.Map<AnswerUser, AnswerUserModel>(answerUserEntity);

            return answerUserModel;
        }


        public AnswerUserModel PostAnswerUser(AnswerUserModel answerUserModel)
        {
            AnswerUser answerUserEntity = _AnswerUserMapper.Map<AnswerUserModel, AnswerUser>(answerUserModel);
            var answerUser = _DAL.PostAnswerUser(answerUserEntity);
            AnswerUserModel answerUserModelReturn = _AnswerUserMapper.Map<AnswerUser, AnswerUserModel>(answerUser);
            return answerUserModelReturn;
        }


        public AnswerUserModel PatchAnswerUser(int id, JsonPatchDocument<AnswerUser> answerUserModelJSON)
        {
            var answerUserEntity = _DAL.PatchAnswerUser(id, answerUserModelJSON);

            AnswerUserModel answerUserModel = _AnswerUserMapper.Map<AnswerUser, AnswerUserModel>(answerUserEntity);

            return answerUserModel;
        }

        public AnswerUserModel PutAnswerUser(AnswerUserModel answerUserModel)
        {
            AnswerUser answerUserEntity = _AnswerUserMapper.Map<AnswerUserModel, AnswerUser>(answerUserModel);
            var answerUser = _DAL.PutAnswerUser(answerUserEntity);
            AnswerUserModel answerUserModelReturn = _AnswerUserMapper.Map<AnswerUser, AnswerUserModel>(answerUser);
            return answerUserModelReturn;
        }
        public void DeleteAnswerUser(int id)
        {
            _DAL.DeleteAnswerUser(id);
        }
    }
}

