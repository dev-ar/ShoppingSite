namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialdb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ReceiverName = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 11),
                        City = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 500),
                        PlateNumber = c.String(nullable: false, maxLength: 10),
                        ZipCode = c.String(nullable: false, maxLength: 10),
                        IdentificationCode = c.String(nullable: false, maxLength: 11),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        IsFinal = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        DetailId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DetailId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductTitle = c.String(nullable: false, maxLength: 300),
                        ShortDescription = c.String(nullable: false, maxLength: 500),
                        Description = c.String(nullable: false),
                        Price = c.Int(nullable: false),
                        ImageName = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductComments",
                c => new
                    {
                        Commentid = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Email = c.String(nullable: false, maxLength: 150),
                        WebSite = c.String(maxLength: 150),
                        Text = c.String(nullable: false, maxLength: 800),
                        CreateDate = c.DateTime(nullable: false),
                        ParentId = c.Int(nullable: true),
                        ProductComments2_Commentid = c.Int(),
                    })
                .PrimaryKey(t => t.Commentid)
                .ForeignKey("dbo.ProductComments", t => t.ProductComments2_Commentid)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ProductComments2_Commentid);
            
            CreateTable(
                "dbo.ProductFeatures",
                c => new
                    {
                        ProductFeatureId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        FeatureId = c.Int(nullable: false),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ProductFeatureId)
                .ForeignKey("dbo.Features", t => t.FeatureId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.FeatureId);
            
            CreateTable(
                "dbo.Features",
                c => new
                    {
                        FeatureId = c.Int(nullable: false, identity: true),
                        FeatureTitle = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.FeatureId);
            
            CreateTable(
                "dbo.ProductGalleries",
                c => new
                    {
                        GalleryId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        GalleryTitle = c.String(nullable: false, maxLength: 250),
                        ImageName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GalleryId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Tag = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.SelectedProductGroups",
                c => new
                    {
                        ProductGroupId = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProductGroupId)
                .ForeignKey("dbo.ProductGroups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.ProductGroups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        GroupTitle = c.String(nullable: false, maxLength: 400),
                        ParentId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.GroupId)
                .ForeignKey("dbo.ProductGroups", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 200),
                        Password = c.String(nullable: false, maxLength: 200),
                        Email = c.String(nullable: false, maxLength: 200),
                        ActiveCode = c.String(nullable: false, maxLength: 200),
                        IsActive = c.Boolean(nullable: false),
                        RegisterDate = c.DateTime(nullable: false),
                        LastLoginIp = c.String(nullable: false, maxLength: 50),
                        LastLoginDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        RoleName = c.String(nullable: false, maxLength: 200),
                        RoleTitle = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.SiteVisits",
                c => new
                    {
                        VisitId = c.Int(nullable: false, identity: true),
                        IP = c.String(nullable: false),
                        VisitDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VisitId);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        SliderId = c.Int(nullable: false, identity: true),
                        SliderTitle = c.String(nullable: false, maxLength: 300),
                        ImageName = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Url = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.SliderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Addresses", "UserId", "dbo.Users");
            DropForeignKey("dbo.SelectedProductGroups", "ProductId", "dbo.Products");
            DropForeignKey("dbo.SelectedProductGroups", "GroupId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductGroups", "ParentId", "dbo.ProductGroups");
            DropForeignKey("dbo.ProductTags", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductGalleries", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductFeatures", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductFeatures", "FeatureId", "dbo.Features");
            DropForeignKey("dbo.ProductComments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductComments", "ProductComments2_Commentid", "dbo.ProductComments");
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.ProductGroups", new[] { "ParentId" });
            DropIndex("dbo.SelectedProductGroups", new[] { "GroupId" });
            DropIndex("dbo.SelectedProductGroups", new[] { "ProductId" });
            DropIndex("dbo.ProductTags", new[] { "ProductId" });
            DropIndex("dbo.ProductGalleries", new[] { "ProductId" });
            DropIndex("dbo.ProductFeatures", new[] { "FeatureId" });
            DropIndex("dbo.ProductFeatures", new[] { "ProductId" });
            DropIndex("dbo.ProductComments", new[] { "ProductComments2_Commentid" });
            DropIndex("dbo.ProductComments", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "AddressId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Addresses", new[] { "UserId" });
            DropTable("dbo.Sliders");
            DropTable("dbo.SiteVisits");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.ProductGroups");
            DropTable("dbo.SelectedProductGroups");
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductGalleries");
            DropTable("dbo.Features");
            DropTable("dbo.ProductFeatures");
            DropTable("dbo.ProductComments");
            DropTable("dbo.Products");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Addresses");
        }
    }
}
