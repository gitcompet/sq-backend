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
            this.IsClosed = false;
            this.LanguageId = 2;
            this.Timer = quizUserModelPostDTO.Timer;
        }
        public string QuizUserId { get; set; }
        public string QuizId { get; set; }
        public string TestUserId { get; set; }
        public int Score { get; set; }
        public Boolean IsClosed { get; set; }
        public int LanguageId { get; set; }
        public Boolean Timer { get; set; }
        public (int, int, float)? ranking { get; set; }
    }
}
