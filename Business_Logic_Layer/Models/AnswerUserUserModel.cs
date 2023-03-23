using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AnswerUserModel
    {
        public AnswerUserModel() { }
        public AnswerUserModel(AnswerUserModelPostDTO answerUserModelPostDTO)
        {
            this.QuestionUserId = answerUserModelPostDTO.QuestionUserId;
            this.AnswerId = answerUserModelPostDTO.AnswerId;
        }
        public string AnswerUserId { get; set; }
        public string QuestionUserId { get; set; }
        public string AnswerId { get; set; }
    }
}
