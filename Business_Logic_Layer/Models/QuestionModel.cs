using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionModel
    {
        public QuestionModel() { }
        public QuestionModel(QuestionModelPostDTO questionModelDTO)
        {
            this.DomainId = questionModelDTO.DomainId;
            this.SubDomainId = questionModelDTO.SubDomainId;
            this.Level = questionModelDTO.Level;
            this.Weight = questionModelDTO.Weight;
            if (questionModelDTO.Duration != null)
            {
                this.Duration = questionModelDTO.Duration;
            }
            else
            {
                this.Duration = -1;
            }
            if (questionModelDTO.Comment != null)
            {
                this.Comment = questionModelDTO.Comment;
            }
            else
            {
                this.Comment = "";
            }
            this.isActive = true;
            this.isDeleted = false;
        }
        public String QuestionId { get; set; }
        public String DomainId { get; set; }
        public String SubDomainId { get; set; }
        public int Level  { get; set; }
        public decimal Weight { get; set; }
        public int Duration { get; set; }
        public String Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
