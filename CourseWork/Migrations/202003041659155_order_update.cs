namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class order_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblOrders", "Confirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblOrders", "Confirmed");
        }
    }
}
