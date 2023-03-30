using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int Level { get; set; }
        public decimal Weight { get; set; }
        public int Duration { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
