using System;
using System.Collections.Generic;
using AutoMapper;
using Business_Logic_Layer.Models;
using Business_Logic_Layer.Interface;
using Data_Access_Layer.Repository.Models;

namespace Business_Logic_Layer
{
    public class UserTypeBLL : InterfaceUserType
    {

        private Data_Access_Layer.UserTypeDAL _DAL;
        private Mapper _UserTypeMapper;

        public UserTypeBLL()
        {
            _DAL = new Data_Access_Layer.UserTypeDAL();
            var _configUserType = new MapperConfiguration(cfg => cfg.CreateMap<UserType, UserTypeModel>().ReverseMap());

            _UserTypeMapper = new Mapper(_configUserType);
        }

        public List<UserTypeModel> GetAllUserType()
        {
            List<UserType> usertypeFromDB = _DAL.GetAllUserType();
            List<UserTypeModel> usertypeModel = _UserTypeMapper.Map<List<UserType>, List<UserTypeModel>>(usertypeFromDB);

            return usertypeModel;
        }

        public UserTypeModel GetUserTypeById(int id)
        {
            var usertypeEntity = _DAL.GetUserTypeById(id);

            UserTypeModel usertypeModel = _UserTypeMapper.Map<UserType, UserTypeModel>(usertypeEntity);

            return usertypeModel;
        }


        public void PostUserType(UserTypeModel usertypeModel)
        {
            UserType usertypeEntity = _UserTypeMapper.Map<UserTypeModel, UserType>(usertypeModel);
            _DAL.postUserType(usertypeEntity);
        }

    }
}


