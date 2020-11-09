namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addstatescities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AllCities", "ParentId", "dbo.AllCities");
            DropIndex("dbo.AllCities", new[] { "ParentId" });
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false),
                        CityTitle = c.String(nullable: false, maxLength: 50),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .Index(t => t.StateId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false),
                        StateTitle = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.StateId);
            
            DropTable("dbo.AllCities");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AllCities",
                c => new
                    {
                        CityId = c.Int(nullable: false),
                        CityTitle = c.String(nullable: false),
                        ParentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId);
            
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropIndex("dbo.Cities", new[] { "StateId" });
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            CreateIndex("dbo.AllCities", "ParentId");
            AddForeignKey("dbo.AllCities", "ParentId", "dbo.AllCities", "CityId");
        }
    }
}
