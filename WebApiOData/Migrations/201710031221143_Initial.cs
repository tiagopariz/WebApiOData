namespace WebApiOData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        EmailId = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmailId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Fullname = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "PersonId", "dbo.People");
            DropIndex("dbo.Emails", new[] { "PersonId" });
            DropTable("dbo.People");
            DropTable("dbo.Emails");
        }
    }
}
