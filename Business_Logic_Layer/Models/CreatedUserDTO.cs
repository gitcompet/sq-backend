using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class CreatedUserDTO
    {  
        public CreatedUserDTO(int id) {
            this.Id = id;
        }
        public int Id { get; set; }
    }
}
