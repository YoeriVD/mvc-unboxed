using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace unboxed.Definition.Questions
{
    [Table("MultipleChoice", Schema = "Definition")]
    public class MultipleChoice : QuestionBase
    {
        public virtual ICollection<Answer> PossibleAnswers { get; set; }

        public MultipleChoice()
        {
            base.QuestionType = "MultipleChoice";
        }
    }
}