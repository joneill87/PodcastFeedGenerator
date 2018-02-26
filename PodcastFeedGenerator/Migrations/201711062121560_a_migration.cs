namespace PodcastFeedGenerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a_migration : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Channels", "FeedDefinitionID");
            AddForeignKey("dbo.Channels", "FeedDefinitionID", "dbo.FeedDefinitions", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Channels", "FeedDefinitionID", "dbo.FeedDefinitions");
            DropIndex("dbo.Channels", new[] { "FeedDefinitionID" });
        }
    }
}
