﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class UserTypeModel
    {
        public String UserTypeId { get; set; }
        public string Description { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}