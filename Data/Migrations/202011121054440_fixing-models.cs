namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingmodels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ProductRate", c => c.Decimal(nullable: true, precision: 18, scale: 2));
            AddColumn("dbo.ProductComments", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.ProductComments", "CommentTitle", c => c.String(maxLength: 150));
            AddColumn("dbo.ProductComments", "Rate", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "ImageName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.ProductGalleries", "ImageName", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.ProductComments", "UserId");
            AddForeignKey("dbo.ProductComments", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            DropColumn("dbo.ProductComments", "Name");
            DropColumn("dbo.ProductComments", "Email");
            DropColumn("dbo.ProductComments", "WebSite");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductComments", "WebSite", c => c.String(maxLength: 150));
            AddColumn("dbo.ProductComments", "Email", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.ProductComments", "Name", c => c.String(nullable: false, maxLength: 100));
            DropForeignKey("dbo.ProductComments", "UserId", "dbo.Users");
            DropIndex("dbo.ProductComments", new[] { "UserId" });
            AlterColumn("dbo.ProductGalleries", "ImageName", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "ImageName", c => c.String(nullable: false));
            DropColumn("dbo.ProductComments", "Rate");
            DropColumn("dbo.ProductComments", "CommentTitle");
            DropColumn("dbo.ProductComments", "UserId");
            DropColumn("dbo.Products", "ProductRate");
        }
    }
}
