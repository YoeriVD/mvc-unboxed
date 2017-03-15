using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unboxed.Definition;

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
}
