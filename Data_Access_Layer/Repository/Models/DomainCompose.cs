﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class DomainCompose
    {
        public int DomainComposeId { get; set; }
        public int DomainId { get; set; }
        public int SubDomainId { get; set; }
    }
}