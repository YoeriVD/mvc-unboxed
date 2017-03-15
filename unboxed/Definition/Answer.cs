using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unboxed.Infrastructure;

namespace unboxed.Definition
{
    [Table("Answers", Schema = "Definition")]
    public class Answer : Entity
    {
        public string Category { get; set; }
        public int Score { get; set; } = 1;
    }
}
