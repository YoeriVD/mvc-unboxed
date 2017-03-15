using System.ComponentModel.DataAnnotations.Schema;

namespace unboxed.Definition.Questions
{
    [Table("YesNo", Schema = "Definition")]
    public class YesNoQuestion : QuestionBase
    {
        public virtual Answer PositiveAnswer { get; set; }
        public virtual Answer NegativeAnswer { get; set; }

        public YesNoQuestion()
        {
            base.QuestionType = "YesNo";
        }
    }
}