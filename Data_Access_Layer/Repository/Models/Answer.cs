using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.Models
{
    public class TestAttribution
    {
        public int TestAttributionId { get; set; }
        public int TestId { get; set; }
        public int LogonId { get; set; }
        public int LanguagesID { get; set; }
        public int TestStatusId { get; set; }
        public Boolean isWithTimer { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TestDate { get; set; }
        public string Comment { get; set; }
        public Boolean isDeleted { get; set; }
    }
}
