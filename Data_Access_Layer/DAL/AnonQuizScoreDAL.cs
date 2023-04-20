using Data_Access_Layer.Repository;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Data_Access_Layer.DAL
{
    public class AnonQuizScoreDAL
    {
        public (int, int, float) GetEvaluation(int score, int id)
        {
            var db = new CompetenceDbContext();
            int beaten;
            int full;
            float percentage;

            beaten = db.AnonQuizScore.Where(x => x.Score <= score && x.QuizId == id).Count();
            full = db.AnonQuizScore.Where(x => x.Score >= -1 && x.QuizId == id).Count();
            percentage = (float)1-((float)beaten / (float)full);
            return (beaten, full, percentage);
        }
    }
}

