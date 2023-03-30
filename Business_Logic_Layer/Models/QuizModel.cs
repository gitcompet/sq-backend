using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuizModel
    {
        public QuizModel() { }
        public QuizModel(QuizModelPostDTO quizModelDTO)
        {
            this.Weight = quizModelDTO.Weight;
            if (quizModelDTO.Comment != null)
            {
                this.Comment = quizModelDTO.Comment;
            }
            else
            {
                this.Comment = "";
            }
            this.isActive = true;
            this.isDeleted = false;
        }
        public string QuizId { get; set; }
        public decimal Weight { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
