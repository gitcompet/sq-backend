using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interface
{
    public interface InterfaceUser
    {
        List<UserModel> GetAllUser();


        UserModel GetUserById(int id);
        int PostUser(UserModelDTO userModelDTO);
    }
}
