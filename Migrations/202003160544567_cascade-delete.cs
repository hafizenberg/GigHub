namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cascadedelete : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Attendences", new[] { "Gig_Id" });
            AlterColumn("dbo.Attendences", "Gig_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Attendences", "Gig_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Attendences", new[] { "Gig_Id" });
            AlterColumn("dbo.Attendences", "Gig_Id", c => c.Int());
            CreateIndex("dbo.Attendences", "Gig_Id");
        }
    }
}
