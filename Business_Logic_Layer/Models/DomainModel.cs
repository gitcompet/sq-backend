using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class DomainModel
    {
        public int DomainId { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }
        public Boolean Deleted { get; set; }
    }
}
