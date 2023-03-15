using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
