using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using unboxed.Infrastructure;

namespace unboxed.Definition.Questions
{
    [Table("Questions", Schema = "Definition")]
    public abstract class QuestionBase : Entity
    {
        public QuestionState State { get; set; } = QuestionState.NotAsked;
        public string QuestionText { get; set; }
        public abstract IEnumerable<Answer> GetAnswers();
    }

    public enum QuestionState
    {
        NotAsked,
        Answered
    }
}