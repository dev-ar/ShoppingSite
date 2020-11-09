namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingStateCities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropPrimaryKey("dbo.Cities");
            DropPrimaryKey("dbo.States");
            AlterColumn("dbo.Cities", "CityId", c => c.Int(nullable: false));
            AlterColumn("dbo.States", "StateId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Cities", "CityId");
            AddPrimaryKey("dbo.States", "StateId");
            AddForeignKey("dbo.Cities", "StateId", "dbo.States", "StateId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cities", "StateId", "dbo.States");
            DropPrimaryKey("dbo.States");
            DropPrimaryKey("dbo.Cities");
            AlterColumn("dbo.States", "StateId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Cities", "CityId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.States", "StateId");
            AddPrimaryKey("dbo.Cities", "CityId");
            AddForeignKey("dbo.Cities", "StateId", "dbo.States", "StateId", cascadeDelete: true);
        }
    }
}
