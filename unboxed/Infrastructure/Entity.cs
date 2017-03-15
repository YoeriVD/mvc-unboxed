using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace unboxed.Infrastructure
{
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
}