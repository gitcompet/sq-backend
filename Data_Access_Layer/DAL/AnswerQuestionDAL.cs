using Data_Access_Layer.Repository;
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
        public Boolean[] GetAnswerQuestionByQuestionId(int id)
        {

            var result = new List<Boolean>();

            var db = new CompetenceDbContext();
            //var d = new List<AnswerQuestion>();
            var d = db.AnswerQuestion.Where(x => x.QuestionId == id).ToList();
            foreach (AnswerQuestion e in d)
            {
                result.Add(e.isAnswerOK);
            }

            return result.ToArray();
        }
        public IEnumerable<string> GetAnswerByListId(int id)
        {

            var result = new List<string>();

            var db = new CompetenceDbContext();
            //var d = new List<AnswerQuestion>();
            var d = db.AnswerQuestion.Where(x => x.QuestionId == id).ToList();
            foreach (AnswerQuestion e in d)
            {
                result.Add(e.AnswerId.ToString());
            }

            return result;
        }

        public void postAnswerQuestion(AnswerQuestion answerquestion)
        {
            var db = new CompetenceDbContext();
            db.Add(answerquestion);
            db.SaveChanges();
        }

    }
}

