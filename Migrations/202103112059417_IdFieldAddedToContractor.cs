namespace TMSCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdFieldAddedToContractor : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Contractors");
            AddColumn("dbo.Contractors", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Contractors", "UserId", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Contractors", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Contractors");
            AlterColumn("dbo.Contractors", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Contractors", "Id");
            AddPrimaryKey("dbo.Contractors", "UserId");
        }
    }
}
