﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class TestStatus
    {
        public int TestStatusId { get; set; }
        public string Description { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
