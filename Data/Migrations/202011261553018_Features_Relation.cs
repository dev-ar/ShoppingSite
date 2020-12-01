namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Features_Relation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Features", "GroupId", c => c.Int(nullable: true));
            CreateIndex("dbo.Features", "GroupId");
            AddForeignKey("dbo.Features", "GroupId", "dbo.ProductGroups", "GroupId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Features", "GroupId", "dbo.ProductGroups");
            DropIndex("dbo.Features", new[] { "GroupId" });
            DropColumn("dbo.Features", "GroupId");
        }
    }
}
