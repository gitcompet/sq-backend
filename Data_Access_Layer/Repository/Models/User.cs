using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class User
    {
        [Key]
        public int LoginId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int AccessFailedCount { get; set; }
        public int LanguageId { get; set; }
        public int TypeUserId { get; set; }
        public DateTime DateCreat { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
