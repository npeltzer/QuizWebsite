using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestPrototype.Models
{
    public class QuizInitializer : CreateDatabaseIfNotExists<QuizContext>
    {
        protected override void Seed(QuizContext context)
        {
            var c = new Topic() { Title = "C#" };
            var q= new MCQuestion() {Question="How many tests are there?", A="1", B="2",C="3",D="4",CorrectAnswer=Answer.C,QuestionTopic=c };
            c.Questions.Add(q);
            context.MCQuestions.Add(q);
            context.Topics.Add(c);
            context.Topics.Add(new Topic() { Title = "HTML"});
            context.Topics.Add(new Topic() { Title = "JavaScript"});
            context.Topics.Add(new Topic() { Title = "SQL"});
   

            context.SaveChanges();

        }
    }

}