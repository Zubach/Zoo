namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblWhoreCnf",
                c => new
                    {
                        PimpID = c.String(nullable: false, maxLength: 128),
                        Confirmed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PimpID);
            
            AddColumn("dbo.AspNetUsers", "PimpConfirmed", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PimpConfirmed");
            DropTable("dbo.tblWhoreCnf");
        }
    }
}
