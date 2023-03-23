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
    public class UserBLL : InterfaceUser
    {

        private UserDAL _DAL;
        private Mapper _UserMapper;

        public UserBLL()
        {
            _DAL = new Data_Access_Layer.DAL.UserDAL();
            var _configUser = new MapperConfiguration(cfg => cfg.CreateMap<User, UserModel>().ReverseMap());

            _UserMapper = new Mapper(_configUser);
        }

        public List<UserModel> GetAllUser()
        {
            List<User> userFromDB = _DAL.GetAllUser();
            List<UserModel> userModel = _UserMapper.Map<List<User>, List<UserModel>>(userFromDB);

            return userModel;
        }

        public UserModel GetUserById(int id)
        {
            var userEntity = _DAL.GetUserById(id);

            UserModel userModel = _UserMapper.Map<User, UserModel>(userEntity);

            return userModel;
        }
        
        public UserModel GetUserByUsername(string username)
        {
            var UserEntity = _DAL.GetUserByUsername(username);

            UserModel UserModel = _UserMapper.Map<User, UserModel>(UserEntity);

            return UserModel;
        }

        public UserModel PostUser(UserModel userModel)
        {
            User userEntity = _UserMapper.Map<UserModel, User>(userModel);
            var user = _DAL.PostUser(userEntity);
            UserModel userModelReturn = _UserMapper.Map<User, UserModel>(user);
            return userModelReturn;
        }


        public UserModel PatchUser(int id, JsonPatchDocument<User> userModelJSON)
        {
            var userEntity = _DAL.PatchUser(id, userModelJSON);

            UserModel userModel = _UserMapper.Map<User, UserModel>(userEntity);

            return userModel;
        }

        public UserModel PutUser(UserModel userModel)
        {
            User userEntity = _UserMapper.Map<UserModel, User>(userModel);
            var user = _DAL.PutUser(userEntity);
            UserModel userModelReturn = _UserMapper.Map<User, UserModel>(user);
            return userModelReturn;
        }
        public void DeleteUser(int id)
        {
            _DAL.DeleteUser(id);
        }
    }
}

