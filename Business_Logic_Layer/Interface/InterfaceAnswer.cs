﻿using Business_Logic_Layer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Logic_Layer.Interface
{
    public interface InterfaceAnswer
    {
            List<AnswerModel> GetAllAnswer();


        AnswerModel GetAnswerById(int id);
            void PostAnswer(AnswerModel answerModel);
        }
    }
