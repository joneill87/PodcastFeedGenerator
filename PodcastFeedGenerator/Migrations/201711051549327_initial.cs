namespace PodcastFeedGenerator.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        ParentCategory = c.String(),
                        Subcategory = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.Channels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FeedDefinitionID = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        WebsiteURL = c.String(),
                        Language = c.String(),
                        CopyrightNotice = c.String(),
                        LastBuildDate = c.DateTime(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                        DocumentationSource = c.String(),
                        WebmasterEmail = c.String(),
                        Author = c.String(),
                        SubTitle = c.String(),
                        Summary = c.String(),
                        OwnerName = c.String(),
                        OwnerEmail = c.String(),
                        IsExplicit = c.Boolean(nullable: false),
                        ImageURL = c.String(),
                        PrimaryCategoryID = c.Int(nullable: false),
                        Category2ID = c.Int(),
                        Category3ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category2ID)
                .ForeignKey("dbo.Categories", t => t.Category3ID)
                .ForeignKey("dbo.Categories", t => t.PrimaryCategoryID, cascadeDelete: true)
                .Index(t => t.PrimaryCategoryID)
                .Index(t => t.Category2ID)
                .Index(t => t.Category3ID);
            
            CreateTable(
                "dbo.FeedDefinitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        XMLNameSpace = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        URL = c.String(),
                        Description = c.String(),
                        FileURL = c.String(),
                        FileSizeInBytes = c.Long(nullable: false),
                        LengthInSeconds = c.Int(nullable: false),
                        MimeType = c.String(),
                        PrimaryCategoryID = c.Int(nullable: false),
                        Category2ID = c.Int(),
                        Category3ID = c.Int(),
                        PublicationDate = c.DateTime(),
                        Author = c.String(),
                        IsExplicit = c.Boolean(nullable: false),
                        SubTitle = c.String(),
                        Summary = c.String(),
                        Channel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category2ID)
                .ForeignKey("dbo.Categories", t => t.Category3ID)
                .ForeignKey("dbo.Channels", t => t.Channel_ID)
                .ForeignKey("dbo.Categories", t => t.PrimaryCategoryID, cascadeDelete: true)
                .Index(t => t.PrimaryCategoryID)
                .Index(t => t.Category2ID)
                .Index(t => t.Category3ID)
                .Index(t => t.Channel_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Items", "PrimaryCategoryID", "dbo.Categories");
            DropForeignKey("dbo.Items", "Channel_ID", "dbo.Channels");
            DropForeignKey("dbo.Items", "Category3ID", "dbo.Categories");
            DropForeignKey("dbo.Items", "Category2ID", "dbo.Categories");
            DropForeignKey("dbo.Channels", "PrimaryCategoryID", "dbo.Categories");
            DropForeignKey("dbo.Channels", "Category3ID", "dbo.Categories");
            DropForeignKey("dbo.Channels", "Category2ID", "dbo.Categories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Items", new[] { "Channel_ID" });
            DropIndex("dbo.Items", new[] { "Category3ID" });
            DropIndex("dbo.Items", new[] { "Category2ID" });
            DropIndex("dbo.Items", new[] { "PrimaryCategoryID" });
            DropIndex("dbo.Channels", new[] { "Category3ID" });
            DropIndex("dbo.Channels", new[] { "Category2ID" });
            DropIndex("dbo.Channels", new[] { "PrimaryCategoryID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Items");
            DropTable("dbo.FeedDefinitions");
            DropTable("dbo.Channels");
            DropTable("dbo.Categories");
        }
    }
}
