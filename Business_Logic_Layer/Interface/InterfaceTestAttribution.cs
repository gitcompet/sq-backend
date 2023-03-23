using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Interface
{
    public interface InterfaceTestAttribution
    {
            List<TestAttributionModel> GetAllTestAttribution();


        TestAttributionModel GetTestAttributionById(int id);
            void PostTestAttribution(TestAttributionModel testattributionModel);
        }
    }
