namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingIPActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastLoginIp", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "ActiveCode", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "ActiveCode", c => c.String(maxLength: 200));
            DropColumn("dbo.Users", "LastLoginIp");
        }
    }
}
