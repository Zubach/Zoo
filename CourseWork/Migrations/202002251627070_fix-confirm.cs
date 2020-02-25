namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixconfirm : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tblWhoreCnf");
            AddColumn("dbo.tblWhoreCnf", "ID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.tblWhoreCnf", "PimpID", c => c.String());
            AddPrimaryKey("dbo.tblWhoreCnf", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tblWhoreCnf");
            AlterColumn("dbo.tblWhoreCnf", "PimpID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.tblWhoreCnf", "ID");
            AddPrimaryKey("dbo.tblWhoreCnf", "PimpID");
        }
    }
}
