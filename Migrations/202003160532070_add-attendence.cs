namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addattendence : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendences",
                c => new
                    {
                        GigsId = c.Int(nullable: false),
                        AttendeeId = c.String(nullable: false, maxLength: 128),
                        Gig_Id = c.Int(),
                    })
                .PrimaryKey(t => new { t.GigsId, t.AttendeeId })
                .ForeignKey("dbo.AspNetUsers", t => t.AttendeeId, cascadeDelete: true)
                .ForeignKey("dbo.Gigs", t => t.Gig_Id)
                .Index(t => t.AttendeeId)
                .Index(t => t.Gig_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendences", "Gig_Id", "dbo.Gigs");
            DropForeignKey("dbo.Attendences", "AttendeeId", "dbo.AspNetUsers");
            DropIndex("dbo.Attendences", new[] { "Gig_Id" });
            DropIndex("dbo.Attendences", new[] { "AttendeeId" });
            DropTable("dbo.Attendences");
        }
    }
}
