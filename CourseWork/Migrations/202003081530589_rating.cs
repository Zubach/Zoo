namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblOrders", "CanRating", c => c.Boolean(nullable: false));
            AddColumn("dbo.tblWhores", "Rating", c => c.Single());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblWhores", "Rating");
            DropColumn("dbo.tblOrders", "CanRating");
        }
    }
}
