namespace FClam5.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using FClam5.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<FClam5.Models.ErrorReportContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FClam5.Models.ErrorReportContext";
        }

        protected override void Seed(FClam5.Models.ErrorReportContext context)
        {
            context.ErrorReports.AddOrUpdate(i => i.reportNumber,
                new ErrorReport
                {
                    reportNumber = 0,
                    errorNumber = 0,
                    URL = "http://spsu.edu/gradstudies%20/",
                    parentURL = "http://spsu.edu/",
                    errorType = "this is stupid"
                },
                new ErrorReport
                {
                    reportNumber = 1,
                    errorNumber = 0,
                    URL = "http://spsu.edu/undergraduate",
                    parentURL = "http://spsu.edu",
                    errorType = "stupid"
                });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
