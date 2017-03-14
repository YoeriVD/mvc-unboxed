using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unboxed
{
    public class UnboxedDbContext : DbContext
    {
        public IDbSet<Survey> Surveys { get; set; }
    }

    public abstract class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid ExternalId { get; set; }

        protected Entity()
        {
            ExternalId = Guid.NewGuid();
        }
    }

    public class Survey : Entity
    {
    }
}
