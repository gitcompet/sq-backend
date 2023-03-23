using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class QuizModelPostDTO
    {
        public string DomainId { get; set; }
        public string SubDomainId { get; set; }
        public decimal Weight { get; set; }
        public string Comment { get; set; }
    }
}
