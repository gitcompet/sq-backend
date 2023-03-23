using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionUserModel
    {
        public QuestionUserModel() { }
        public QuestionUserModel(QuestionUserModelPostDTO questionUserModelPostDTO)
        {
            this.QuestionId = questionUserModelPostDTO.QuestionId;
            this.QuizUserId = questionUserModelPostDTO.QuizUserId;
        }
        public string QuestionUserId { get; set; }
        public string QuestionId { get; set; }
        public string QuizUserId { get; set; }
    }
}
