﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestAttributionModel
    {
        public String TestAttributionId { get; set; }
        public string TestId { get; set; }
        public string LogonId { get; set; }
        public string LanguagesID { get; set; }
        public string TestStatusId { get; set; }
        public Boolean isWithTimer { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TestDate { get; set; }
        public string Comment { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
