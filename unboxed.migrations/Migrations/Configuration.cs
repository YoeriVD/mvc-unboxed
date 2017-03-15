using unboxed.Definition;
using unboxed.TestData;

namespace unboxed.migrations.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<unboxed.UnboxedDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(unboxed.UnboxedDbContext context)
        {
            if (context.Surveys.Any()) return;
            context.Surveys.Add(TestDataFactory.GetSurveyWithPredefinedQuestions());
            context.SaveChanges();
        }
    }
}
