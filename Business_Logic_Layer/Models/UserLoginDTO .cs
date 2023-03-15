using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class UserLoginDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }      
    }
}
