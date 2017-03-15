using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unboxed.Definition.Questions
{
    [Table("YesNo", Schema = "Definition")]
    public class YesNoQuestion : QuestionBase
    {
        public Answer PositiveAnswer { get; set; }
        public Answer NegativeAnswer { get; set; }
        public bool? Answer { get; set; }
        public override IEnumerable<Answer> GetAnswers()
        {
            yield return Answer.HasValue
                ? Answer.Value ? PositiveAnswer : NegativeAnswer
                : null;
        }
    }
}
