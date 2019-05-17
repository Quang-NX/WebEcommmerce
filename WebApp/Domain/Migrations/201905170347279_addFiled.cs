namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFiled : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FistName", c => c.String());
            AddColumn("dbo.Customers", "LastName", c => c.String());
            DropColumn("dbo.Customers", "CustomerName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "CustomerName", c => c.String());
            DropColumn("dbo.Customers", "LastName");
            DropColumn("dbo.Customers", "FistName");
        }
    }
}
