namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixing : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.ProductComments", new[] { "ProductComments2_Commentid" });
            DropColumn("dbo.ProductComments", "ParentId");
            RenameColumn(table: "dbo.ProductComments", name: "ProductComments2_Commentid", newName: "ParentId");
            AlterColumn("dbo.ProductComments", "ParentId", c => c.Int(nullable: true));
            AlterColumn("dbo.ProductComments", "ParentId", c => c.Int(nullable: true));
            CreateIndex("dbo.ProductComments", "ParentId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ProductComments", new[] { "ParentId" });
            AlterColumn("dbo.ProductComments", "ParentId", c => c.Int());
            AlterColumn("dbo.ProductComments", "ParentId", c => c.Int());
            RenameColumn(table: "dbo.ProductComments", name: "ParentId", newName: "ProductComments2_Commentid");
            AddColumn("dbo.ProductComments", "ParentId", c => c.Int());
            CreateIndex("dbo.ProductComments", "ProductComments2_Commentid");
        }
    }
}
