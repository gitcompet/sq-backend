using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Interface
{
    public interface InterfaceUserType
    {
            List<UserTypeModel> GetAllUserType();


            UserTypeModel GetUserTypeById(int id);
            void PostUserType(UserTypeModel usertypeModel);
        }
    }
