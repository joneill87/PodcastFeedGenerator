namespace PodcastFeedGenerator.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PodcastFeedGenerator.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<PodcastFeedGenerator.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public void RunSeedMethod()
        {
            Seed(new ApplicationDbContext());
        }

        protected override void Seed(PodcastFeedGenerator.Models.ApplicationDbContext context)
        {
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
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser { UserName = "fergus", Email = "fergus.gildea@utrack.com"};
            Task<IdentityResult> createUserTask = userManager.CreateAsync(user, "alanfergusjackmarktim");
            createUserTask.Wait();


        }
    }
}
