using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionMoreModelPutDTO
    {
        public String QuestionId { get; set; }
        public String DomainId { get; set; }
        public String SubDomainId { get; set; }
        public int Level  { get; set; }
        public decimal Weight { get; set; }
        public int Duration { get; set; }
        public String Comment { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }

        //Answer
        public String[] AnswerTextList { get; set; }

        //Answer Question
        public String[] AnswerCheckList { get; set; }
    }
}
