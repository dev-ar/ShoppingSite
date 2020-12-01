namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Features", "GroupId", "dbo.ProductGroups");
            DropIndex("dbo.Features", new[] { "GroupId" });
            AlterColumn("dbo.Features", "GroupId", c => c.Int());
            CreateIndex("dbo.Features", "GroupId");
            AddForeignKey("dbo.Features", "GroupId", "dbo.ProductGroups", "GroupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Features", "GroupId", "dbo.ProductGroups");
            DropIndex("dbo.Features", new[] { "GroupId" });
            AlterColumn("dbo.Features", "GroupId", c => c.Int(nullable: false));
            CreateIndex("dbo.Features", "GroupId");
            AddForeignKey("dbo.Features", "GroupId", "dbo.ProductGroups", "GroupId", cascadeDelete: true);
        }
    }
}
