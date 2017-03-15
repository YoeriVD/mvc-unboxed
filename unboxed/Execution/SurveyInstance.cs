using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using unboxed.Definition;
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

        public Survey BasedOn { get; set; }
        public virtual ICollection<QuestionInstance> QuestionInstances { get; set; }
    }

    [Table("Question", Schema = "Execution")]
    public class QuestionInstance : Entity
    {
        public QuestionState State { get; set; } = QuestionState.NotAsked;
    }
}