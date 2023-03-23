using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class QuestionUser
    {
        public int QuestionUserId { get; set; }
        public int QuestionId { get; set; }
        public int QuizUserId { get; set; }
    }
}
