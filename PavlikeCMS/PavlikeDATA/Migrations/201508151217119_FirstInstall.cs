namespace PavlikeDATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstInstall : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumMedia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbumId = c.Int(nullable: false),
                        MediaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Album", t => t.AlbumId)
                .ForeignKey("dbo.Media", t => t.MediaId)
                .Index(t => t.AlbumId)
                .Index(t => t.MediaId);
            
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserGuid = c.String(),
                        RoleGuid = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        DateofBirth = c.DateTime(),
                        EMail = c.String(),
                        PictureId = c.Int(),
                        Active = c.Boolean(nullable: false),
                        Media_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.Media_Id)
                .ForeignKey("dbo.Media", t => t.PictureId)
                .Index(t => t.PictureId)
                .Index(t => t.Media_Id);
            
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(),
                        PageId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        ArticleTypeId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ArticleType", t => t.ArticleTypeId)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .ForeignKey("dbo.Page", t => t.PageId)
                .Index(t => t.PageId)
                .Index(t => t.AuthorId)
                .Index(t => t.ArticleTypeId);
            
            CreateTable(
                "dbo.ArticleType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Active = c.Boolean(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Page",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Url = c.String(nullable: false),
                        RootPageId = c.Int(),
                        AuthorId = c.Int(nullable: false),
                        Published = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        PageOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .ForeignKey("dbo.Page", t => t.RootPageId)
                .Index(t => t.RootPageId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.File",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Detail = c.String(),
                        Folder = c.String(),
                        Url = c.String(),
                        FileName = c.String(),
                        Extension = c.String(),
                        UploadDateTime = c.DateTime(nullable: false),
                        FileType = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.Document",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        AuthorId = c.Int(),
                        FileId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Published = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .ForeignKey("dbo.File", t => t.FileId)
                .Index(t => t.AuthorId)
                .Index(t => t.FileId);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        AltText = c.String(),
                        Description = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        FileId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Author_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .ForeignKey("dbo.File", t => t.FileId)
                .ForeignKey("dbo.Author", t => t.Author_Id)
                .Index(t => t.AuthorId)
                .Index(t => t.FileId)
                .Index(t => t.Author_Id);
            
            CreateTable(
                "dbo.Slider",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SubTitle = c.String(),
                        Detail = c.String(),
                        FileId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .ForeignKey("dbo.File", t => t.FileId)
                .Index(t => t.FileId)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.EntityLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ErrorId = c.Int(nullable: false),
                        Detail = c.String(),
                        Class = c.String(),
                        Method = c.String(),
                        EntityModel = c.String(),
                        Job = c.Int(nullable: false),
                        EntityResult = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Seo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.Int(nullable: false),
                        Description = c.String(),
                        Keywords = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        MetaTags = c.String(),
                        Url = c.String(),
                        AdminEmail = c.String(),
                        MailServer = c.String(),
                        MailPort = c.Int(nullable: false),
                        SenderEMail = c.String(),
                        SenderPassword = c.String(),
                        SenderDisplayName = c.String(),
                        MailServerSsl = c.Boolean(nullable: false),
                        LogoId = c.Int(),
                        SliderHeight = c.Int(),
                        SliderWidht = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.LogoId)
                .Index(t => t.LogoId);
            
            CreateTable(
                "dbo.Social",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ApiKey = c.String(),
                        ApiCode = c.String(),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
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
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Settings", "LogoId", "dbo.Media");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Author", "PictureId", "dbo.Media");
            DropForeignKey("dbo.Media", "Author_Id", "dbo.Author");
            DropForeignKey("dbo.Slider", "FileId", "dbo.File");
            DropForeignKey("dbo.Slider", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Media", "FileId", "dbo.File");
            DropForeignKey("dbo.Author", "Media_Id", "dbo.Media");
            DropForeignKey("dbo.Media", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.AlbumMedia", "MediaId", "dbo.Media");
            DropForeignKey("dbo.Document", "FileId", "dbo.File");
            DropForeignKey("dbo.Document", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.File", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Page", "RootPageId", "dbo.Page");
            DropForeignKey("dbo.Page", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Article", "PageId", "dbo.Page");
            DropForeignKey("dbo.Article", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.ArticleType", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.Article", "ArticleTypeId", "dbo.ArticleType");
            DropForeignKey("dbo.Album", "AuthorId", "dbo.Author");
            DropForeignKey("dbo.AlbumMedia", "AlbumId", "dbo.Album");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Settings", new[] { "LogoId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Slider", new[] { "AuthorId" });
            DropIndex("dbo.Slider", new[] { "FileId" });
            DropIndex("dbo.Media", new[] { "Author_Id" });
            DropIndex("dbo.Media", new[] { "FileId" });
            DropIndex("dbo.Media", new[] { "AuthorId" });
            DropIndex("dbo.Document", new[] { "FileId" });
            DropIndex("dbo.Document", new[] { "AuthorId" });
            DropIndex("dbo.File", new[] { "AuthorId" });
            DropIndex("dbo.Page", new[] { "AuthorId" });
            DropIndex("dbo.Page", new[] { "RootPageId" });
            DropIndex("dbo.ArticleType", new[] { "AuthorId" });
            DropIndex("dbo.Article", new[] { "ArticleTypeId" });
            DropIndex("dbo.Article", new[] { "AuthorId" });
            DropIndex("dbo.Article", new[] { "PageId" });
            DropIndex("dbo.Author", new[] { "Media_Id" });
            DropIndex("dbo.Author", new[] { "PictureId" });
            DropIndex("dbo.Album", new[] { "AuthorId" });
            DropIndex("dbo.AlbumMedia", new[] { "MediaId" });
            DropIndex("dbo.AlbumMedia", new[] { "AlbumId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Social");
            DropTable("dbo.Settings");
            DropTable("dbo.Seo");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.EntityLog");
            DropTable("dbo.Slider");
            DropTable("dbo.Media");
            DropTable("dbo.Document");
            DropTable("dbo.File");
            DropTable("dbo.Page");
            DropTable("dbo.ArticleType");
            DropTable("dbo.Article");
            DropTable("dbo.Author");
            DropTable("dbo.Album");
            DropTable("dbo.AlbumMedia");
        }
    }
}
