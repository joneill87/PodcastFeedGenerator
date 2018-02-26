namespace PodcastFeedGenerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itemKeywords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "Keywords", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Items", "Keywords");
        }
    }
}
