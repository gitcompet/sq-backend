using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class SubDomainModel
    {
        public SubDomainModel() { }
        public SubDomainModel(SubDomainModelPostDTO subDomainModelPost)
        {
            if (subDomainModelPost.Name != null)
            {
                this.Name = subDomainModelPost.Name;
            }
            else
            {
                this.Name = "";
            }
            this.isActive = true;
            this.isDeleted = false;
        }
        public String SubDomainId { get; set; }
        public string Name { get; set; }
        public Boolean isActive { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
