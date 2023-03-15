using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class TestCompose
    { 
        public int TestComposeId { get; set; }
        public int TestId { get; set; }
        public int QuizId { get; set; }
    }
}
