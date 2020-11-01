namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingaddresstoOrders : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "AddressId");
            AddForeignKey("dbo.Orders", "AddressId", "dbo.Addresses", "AddressId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Orders", new[] { "AddressId" });
            DropColumn("dbo.Orders", "AddressId");
        }
    }
}
