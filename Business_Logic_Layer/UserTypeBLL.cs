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
    public class UserTypeBLL : InterfaceUserType
    {

        private UserTypeDAL _DAL;
        private Mapper _UserTypeMapper;

        public UserTypeBLL()
        {
            _DAL = new Data_Access_Layer.DAL.UserTypeDAL();
            var _configUserType = new MapperConfiguration(cfg => cfg.CreateMap<UserType, UserTypeModel>().ReverseMap());

            _UserTypeMapper = new Mapper(_configUserType);
        }

        public List<UserTypeModel> GetAllUserType()
        {
            List<UserType> userTypeFromDB = _DAL.GetAllUserType();
            List<UserTypeModel> userTypeModel = _UserTypeMapper.Map<List<UserType>, List<UserTypeModel>>(userTypeFromDB);

            return userTypeModel;
        }

        public UserTypeModel GetUserTypeById(int id)
        {
            var userTypeEntity = _DAL.GetUserTypeById(id);

            UserTypeModel userTypeModel = _UserTypeMapper.Map<UserType, UserTypeModel>(userTypeEntity);

            return userTypeModel;
        }


        public UserTypeModel PostUserType(UserTypeModel userTypeModel)
        {
            UserType userTypeEntity = _UserTypeMapper.Map<UserTypeModel, UserType>(userTypeModel);
            var userType = _DAL.PostUserType(userTypeEntity);
            UserTypeModel userTypeModelReturn = _UserTypeMapper.Map<UserType, UserTypeModel>(userType);
            return userTypeModelReturn;
        }


        public UserTypeModel PatchUserType(int id, JsonPatchDocument<UserType> userTypeModelJSON)
        {
            var userTypeEntity = _DAL.PatchUserType(id, userTypeModelJSON);

            UserTypeModel userTypeModel = _UserTypeMapper.Map<UserType, UserTypeModel>(userTypeEntity);

            return userTypeModel;
        }

        public UserTypeModel PutUserType(UserTypeModel userTypeModel)
        {
            UserType userTypeEntity = _UserTypeMapper.Map<UserTypeModel, UserType>(userTypeModel);
            var userType = _DAL.PutUserType(userTypeEntity);
            UserTypeModel userTypeModelReturn = _UserTypeMapper.Map<UserType, UserTypeModel>(userType);
            return userTypeModelReturn;
        }
        public void DeleteUserType(int id)
        {
            _DAL.DeleteUserType(id);
        }
    }
}

