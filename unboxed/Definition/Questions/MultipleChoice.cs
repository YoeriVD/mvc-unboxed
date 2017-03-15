using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace unboxed.Definition.Questions
{
    [Table("MultipleChoice", Schema = "Definition")]
    public class MultipleChoice : QuestionBase
    {
        public virtual ICollection<Answer> PossibleAnswers { get; set; }
        public virtual ICollection<Answer> ChosenAnswers { get; set; }

        public override IEnumerable<Answer> GetAnswers()
        {
            return ChosenAnswers.ToList();
        }
    }
}