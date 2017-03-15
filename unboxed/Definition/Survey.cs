using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using unboxed.Definition.Questions;
using unboxed.Infrastructure;

namespace unboxed.Definition
{
    [Table("Survey", Schema = "Definition")]
    public class Survey : Entity
    {
        public Survey()
        {
            Questions = new List<QuestionBase>();
        }

        public string Title { get; set; } = "Survey";
        public ICollection<QuestionBase> Questions { get; set; }
    }
}