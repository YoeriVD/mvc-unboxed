using System.ComponentModel.DataAnnotations.Schema;

namespace unboxed.Definition.Questions
{
    [Table("YesNo", Schema = "Definition")]
    public class YesNoQuestion : QuestionBase
    {
        public Answer PositiveAnswer { get; set; }
        public Answer NegativeAnswer { get; set; }
    }
}