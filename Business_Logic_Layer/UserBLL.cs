using AutoMapper;
using Business_Logic_Layer.Interface;
using Business_Logic_Layer.Models;
using Data_Access_Layer.DAL;
using Data_Access_Layer.Repository.Models;
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


        public int PostUser(UserModelDTO userModelDTO)
        {
            var userModel = new UserModel(userModelDTO);
            System.Diagnostics.Debug.WriteLine("==============================================");
            System.Diagnostics.Debug.WriteLine(userModel.LoginId);
            System.Diagnostics.Debug.WriteLine(userModel.DateCreat);
            System.Diagnostics.Debug.WriteLine("==============================================");
            User languagesEntity = _UserMapper.Map<UserModel, User>(userModel);
            System.Diagnostics.Debug.WriteLine("==============================================");
            System.Diagnostics.Debug.WriteLine(languagesEntity.LoginId);
            System.Diagnostics.Debug.WriteLine(languagesEntity.DateCreat);
            System.Diagnostics.Debug.WriteLine("==============================================");
            return _DAL.postUser(languagesEntity);
        }

    }
}
