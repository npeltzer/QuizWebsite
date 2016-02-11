using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TestPrototype.Models
{
    
    public class QuizContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public QuizContext() : base("name=QuizContext")
        {
            Database.SetInitializer<QuizContext>(new QuizInitializer());
        }

        public DbSet<Quiz> Quizs { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<MCQuestion> MCQuestions { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<Assesment> Assesments { get; set; }
    }
}
