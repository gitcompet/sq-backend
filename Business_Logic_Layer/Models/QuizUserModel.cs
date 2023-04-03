using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuizUserModel
    {
        public QuizUserModel() { }
        public QuizUserModel(QuizUserModelPostDTO quizUserModelPostDTO)
        {
            this.QuizId = quizUserModelPostDTO.QuizId;
            this.TestUserId = quizUserModelPostDTO.TestUserId;
        }
        public string QuizUserId { get; set; }
        public string QuizId { get; set; }
        public string TestUserId { get; set; }
        public int Score { get; set; }
    }
}
