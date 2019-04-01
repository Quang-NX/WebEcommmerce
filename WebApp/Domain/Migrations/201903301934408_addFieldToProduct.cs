namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFieldToProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "productInStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "productInStock");
        }
    }
}
