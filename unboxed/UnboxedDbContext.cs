using System.Data.Entity;
using unboxed.Definition;
using unboxed.Execution;

namespace unboxed
{
    public class UnboxedDbContext : DbContext
    {
        public UnboxedDbContext() : base("DefaultConnection")
        {
        }

        public IDbSet<Survey> Surveys { get; set; }
        public IDbSet<SurveyInstance> SurveyInstances { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder
                .Properties<string>()
                .Configure(s => s.HasMaxLength(255));
                //als je dit niet doet, is elke string standaard NVARCHAR(MAX) => performance hits everywhere!
        }
    }
}