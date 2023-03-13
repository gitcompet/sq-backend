using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class Domain
    {
        public int DomainId { get; set; }
        public string Name { get; set; }
        public Boolean Active { get; set; }
        public Boolean Deleted { get; set; }
    }
}
