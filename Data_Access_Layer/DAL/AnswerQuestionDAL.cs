﻿using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Data_Access_Layer
{
    public class AnswerQuestionDAL
    {
        public List<AnswerQuestion> GetAllAnswerQuestion()
        {
            var db = new CompetenceDbContext();
            return db.AnswerQuestion.ToList();
        }

        public AnswerQuestion GetAnswerQuestionById(int id)
        {
            var db = new CompetenceDbContext();
            AnswerQuestion d = new AnswerQuestion();

            d = db.AnswerQuestion.FirstOrDefault(x => x.AnswerQuestionId == id);

            return d;
        }


        public void postAnswerQuestion(AnswerQuestion answerquestion)
        {
            var db = new CompetenceDbContext();
            db.Add(answerquestion);
            db.SaveChanges();
        }

    }
}
