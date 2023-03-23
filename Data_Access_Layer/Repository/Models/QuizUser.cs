using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class QuizUser
    {
        public int QuizUserId { get; set; }
        public int QuizId { get; set; }
        public int TestUserId { get; set; }
    }
}
