namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cities : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllCities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityTitle = c.String(nullable: false),
                        ParentId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.AllCities", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AllCities", "ParentId", "dbo.AllCities");
            DropIndex("dbo.AllCities", new[] { "ParentId" });
            DropTable("dbo.AllCities");
        }
    }
}
