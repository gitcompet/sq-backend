using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class UserType
    {
//            public string Convert.ToString(UserTypeId) { get; set; } test
            public int UserTypeId { get; set; }
            public string Description { get; set; }
            public Boolean isActive { get; set; }
            public Boolean isDeleted { get; set; }
        }
    }
