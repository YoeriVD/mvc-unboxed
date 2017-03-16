using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using unboxed.Definition;
using unboxed.Definition.Questions;
using unboxed.Infrastructure;

namespace unboxed.Execution
{
    [Table("Survey", Schema = "Execution")]
    public class SurveyInstance : Entity
    {
        public SurveyInstance()
        {
            QuestionInstances = new List<QuestionInstance>();
        }

        public virtual Survey BasedOn { get; set; }
        public virtual ICollection<QuestionInstance> QuestionInstances { get; set; }
    }

    [Table("Question", Schema = "Execution")]
    public class QuestionInstance : Entity
    {
        public QuestionState State { get; set; } = QuestionState.NotAsked;
        public virtual QuestionBase Question { get; set; }
        public string Answer { get; set; }
    }
}