using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class UserTypeModel
    {
        public UserTypeModel() { }
        public UserTypeModel(UserTypeModelPostDTO userTypeModelPostDTO)
        {
            if (userTypeModelPostDTO.Description != null)
            {
                this.Description = userTypeModelPostDTO.Description;
            }
            else
            {
                this.Description = "";
            }
            this.isActive = true;
            this.isDeleted = false;
        }
        public String UserTypeId { get; set; }
        public string Description { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
