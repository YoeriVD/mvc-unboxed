using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using unboxed.Infrastructure;

namespace unboxed.Definition.Questions
{
    [Table("Questions", Schema = "Definition")]
    public abstract class QuestionBase : Entity
    {
        public string QuestionText { get; set; }
    }
}