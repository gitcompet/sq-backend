using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Models
{
    public class TestUserModel
    {
        public TestUserModel() { }
        public TestUserModel(TestUserModelPostDTO testUserModelPostDTO)
        {
            this.LoginId = testUserModelPostDTO.LoginId;
            this.TestId = testUserModelPostDTO.TestId;
            this.TestStatus = testUserModelPostDTO.TestStatus;
        }
        public string TestUserId { get; set; }
        public string LoginId { get; set; }
        public string TestId { get; set; }
        public int TestStatus { get; set; }
    }
}
