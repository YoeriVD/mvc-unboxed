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

        public UnboxedDbContext() : base("DefaultConnection"){

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Properties<string>()
                .Configure(s => s.HasMaxLength(255)); //als je dit niet doet, is elke string standaard NVARCHAR(MAX) => performance hits everywhere!
        }
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
        public string Title { get; set; }
    }
}
