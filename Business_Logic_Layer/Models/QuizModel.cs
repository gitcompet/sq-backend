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
            this.DomainId = quizModelDTO.DomainId;
            this.SubDomainId = quizModelDTO.SubDomainId;
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
        public String QuizId { get; set; }
        public String DomainId { get; set; }
        public String SubDomainId { get; set; }
        public decimal Weight { get; set; }
        public String Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
