using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTestStatus
    {

        List<TestStatusModel> GetAllTestStatus();


        TestStatusModel GetTestStatusById(int id);
        void PostTestStatus(TestStatusModel teststatusModel);
    }
}
