using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class AnswerModel
    {
        public AnswerModel() { }
        public AnswerModel(AnswerModelPostDTO answerModelPostDTO)
        {
            if (answerModelPostDTO.Comment != null)
            {
                this.Comment = answerModelPostDTO.Comment;
            }
            else
            {
                this.Comment = "";
            }
            this.isActive = true;
            this.isDeleted = false;
        }
        public string AnswerId { get; set; }
        public string Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
