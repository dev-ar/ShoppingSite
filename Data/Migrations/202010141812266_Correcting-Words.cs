namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectingWords : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Users", "LastLoginIp", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Users", "Eamil");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Eamil", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Users", "LastLoginIp", c => c.String(nullable: false));
            DropColumn("dbo.Users", "Email");
        }
    }
}
