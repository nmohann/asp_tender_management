namespace TMSCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TenderApplicationAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TenderApplications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentTenderId = c.String(),
                        CurrentUserId = c.String(),
                        Quotation = c.Double(nullable: false),
                        ImagePath = c.String(),
                        IsEvaluated = c.String(),
                        IsApproved = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TenderApplications");
        }
    }
}
