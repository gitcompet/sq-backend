using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class QuizCompose
    {
        [Key]
        public int TestComposeId { get; set; }
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
        public int QuestionLevel { get; set; }
        public decimal QuestionWeight { get; set; }
    }
}
