namespace PodcastFeedGenerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class channel_id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "Channel_ID", "dbo.Channels");
            DropIndex("dbo.Items", new[] { "Channel_ID" });
            RenameColumn(table: "dbo.Items", name: "Channel_ID", newName: "ChannelID");
            AlterColumn("dbo.Items", "ChannelID", c => c.Int(nullable: false));
            CreateIndex("dbo.Items", "ChannelID");
            AddForeignKey("dbo.Items", "ChannelID", "dbo.Channels", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ChannelID", "dbo.Channels");
            DropIndex("dbo.Items", new[] { "ChannelID" });
            AlterColumn("dbo.Items", "ChannelID", c => c.Int());
            RenameColumn(table: "dbo.Items", name: "ChannelID", newName: "Channel_ID");
            CreateIndex("dbo.Items", "Channel_ID");
            AddForeignKey("dbo.Items", "Channel_ID", "dbo.Channels", "ID");
        }
    }
}
