namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateattendence : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Attendences", name: "Gig_Id", newName: "GigId");
            RenameIndex(table: "dbo.Attendences", name: "IX_Gig_Id", newName: "IX_GigId");
            DropPrimaryKey("dbo.Attendences");
            AddPrimaryKey("dbo.Attendences", new[] { "GigId", "AttendeeId" });
            DropColumn("dbo.Attendences", "GigsId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Attendences", "GigsId", c => c.Int(nullable: false));
            DropPrimaryKey("dbo.Attendences");
            AddPrimaryKey("dbo.Attendences", new[] { "GigsId", "AttendeeId" });
            RenameIndex(table: "dbo.Attendences", name: "IX_GigId", newName: "IX_Gig_Id");
            RenameColumn(table: "dbo.Attendences", name: "GigId", newName: "Gig_Id");
        }
    }
}
