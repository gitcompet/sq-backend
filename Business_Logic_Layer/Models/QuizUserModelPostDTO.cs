using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuizUserModelPostDTO
    {
        public string QuizId { get; set; }
        public string TestUserId { get; set; }
        public Boolean Timer { get; set; }
    }
}
