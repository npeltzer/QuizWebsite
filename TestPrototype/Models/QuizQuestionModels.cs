using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TestPrototype.Models
{
    public enum Answer {NoAnswer, A, B, C, D };
    public class Assesment
    {
        public Assesment()
        {
            Quizzes = new List<Quiz>();
        }
        [Key] public int id { get; set; }
        public String Name { get; set; }
        public int Code { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
    }
  
    public class Quiz
    {
        public Quiz()
        {
            Questions = new List<QuizQuestion>();
        }
        [Key]public int id { get; set; }
      [Display (Name="Topic")]
        public virtual Topic QuestionTopic { get; set; }
        public virtual ICollection<QuizQuestion> Questions { get; set; }
        public int TopicId { get; set; }
        public int QuizLength { get; set; }
    }
    public class Topic
    {
        public Topic()
        {
            Quizzes = new List<Quiz>();
            Questions = new List<MCQuestion>();
        }
        [Key]public int id { get; set; }
       
        public String Title { get; set; }
        public virtual ICollection<Quiz> Quizzes { get; set; }
        public virtual ICollection<MCQuestion> Questions { get; set; }
    }
    public class MCQuestion
    {
        [Key] public int id { get; set; }
        public String Question { get; set; }
        public String A { get; set; }
        public String B { get; set; }
        public String C { get; set; }
        public String D { get; set; }
        public Answer CorrectAnswer { get; set; }
        public virtual Topic QuestionTopic { get; set; }
        public int TopicId { get; set; }
    }
    public class QuizQuestion
    {
        [Key] public int id { get; set; }
        public Answer UserAnswer { get; set; }
        
        public virtual MCQuestion question { get; set; }
    }
}