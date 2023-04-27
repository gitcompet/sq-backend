using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class UserModelNoPassword
    {
        public UserModelNoPassword(){}
        public UserModelNoPassword(UserModel userModel)
        {
            this.LoginId = userModel.LoginId;
            this.Login = userModel.Login;
            this.LastName = userModel.LastName;
            this.FirstName = userModel.FirstName;
            this.Email = userModel.Email;
            this.Comment = userModel.Comment;
            this.AccessFailedCount = 0;       
            this.LanguageId = userModel.LanguageId;
            this.TypeUserId = userModel.TypeUserId; ;
            this.DateCreat = userModel.DateCreat;
            this.isActive = userModel.isActive;
            this.isDeleted = userModel.isDeleted;
        }
        public string LoginId { get; set; }
        public string Login { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int AccessFailedCount { get; set; }
        public string LanguageId { get; set; }
        public string TypeUserId { get; set; }
        public DateTime DateCreat { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
