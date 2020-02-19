namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblWhores",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        PricePerHour = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PimpID = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblWhores");
        }
    }
}
