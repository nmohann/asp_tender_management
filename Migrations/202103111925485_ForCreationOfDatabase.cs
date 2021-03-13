namespace TMSCodeFirst.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForCreationOfDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 15),
                        LastName = c.String(nullable: false, maxLength: 15),
                        DOB = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        SecretQuestion = c.String(),
                        SecretAnswer = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Tenders",
                c => new
                    {
                        TenderId = c.String(nullable: false, maxLength: 128),
                        TenderName = c.String(nullable: false),
                        TenderType = c.String(nullable: false),
                        TenderStartDate = c.DateTime(nullable: false),
                        TenderEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TenderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tenders");
            DropTable("dbo.Contractors");
            DropTable("dbo.Admins");
        }
    }
}
