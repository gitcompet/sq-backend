using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class UserModel
    {
        public UserModel(){}
        public UserModel(UserModelDTO userModelDTO)
        {
            this.Login = userModelDTO.Login;
            this.Password = userModelDTO.Password;
            this.LastName = userModelDTO.LastName;
            this.FirstName = userModelDTO.FirstName;
            this.Email = userModelDTO.Email;
            this.Comment = userModelDTO.Comment == null ? "NULL" : userModelDTO.Comment;
            this.AccessFailedCount = 0;       
            this.LanguageId = userModelDTO.LanguageId == null ? "0" : userModelDTO.LanguageId;
            this.TypeUserId = userModelDTO.TypeUserId == null ? "0" : userModelDTO.TypeUserId; ;
            this.DateCreat = DateTime.Now;
            this.IsActive = true;
            this.IsDeleted = false;
        }
        public String LoginId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int AccessFailedCount { get; set; }
        public string LanguageId { get; set; }
        public string TypeUserId { get; set; }
        public DateTime DateCreat { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
