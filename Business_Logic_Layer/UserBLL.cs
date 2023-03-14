using AutoMapper;
using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Data_Access_Layer.DAL;
using Data_Access_Layer.Repository.Models;
using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            List<User> UserFromDB = _DAL.GetAllUser();
            List<UserModel> personsModel = _UserMapper.Map<List<User>, List<UserModel>>(UserFromDB);

            return personsModel;
        }

        public UserModel GetUserById(int id)
        {
            var UserEntity = _DAL.GetUserById(id);

            UserModel UserModel = _UserMapper.Map<User, UserModel>(UserEntity);

            return UserModel;
        }

        public UserModel GetUserByUsername(string username)
        {
            var UserEntity = _DAL.GetUserByUsername(username);

            UserModel UserModel = _UserMapper.Map<User, UserModel>(UserEntity);

            return UserModel;
        }


        public CreatedUserDTO PostUser(UserModelDTO userModelDTO)
        {
            var userModel = new UserModel(userModelDTO);
            string passwordHash
                = BCrypt.Net.BCrypt.HashPassword(userModel.Password);
            userModel.Password = passwordHash;
            System.Diagnostics.Debug.WriteLine("==============================================");
            System.Diagnostics.Debug.WriteLine(userModel.LoginId);
            System.Diagnostics.Debug.WriteLine(userModel.DateCreat);
            System.Diagnostics.Debug.WriteLine("==============================================");
            User userEntity = _UserMapper.Map<UserModel, User>(userModel);
            System.Diagnostics.Debug.WriteLine("==============================================");
            System.Diagnostics.Debug.WriteLine(userEntity.LoginId);
            System.Diagnostics.Debug.WriteLine(userEntity.DateCreat);
            System.Diagnostics.Debug.WriteLine("==============================================");
            int userId = _DAL.postUser(userEntity);
            return new CreatedUserDTO(userId);
        }

    }
}
