using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuestionMoreModelPostDTO
    {
        public int Level  { get; set; }
        public decimal Weight { get; set; }
        public int Duration { get; set; }
        public string Comment { get; set; }

        //Answer
        public string[] AnswerTextList { get; set; }

        //Answer Question
        public string[] AnswerCheckList { get; set; }
    }
}
