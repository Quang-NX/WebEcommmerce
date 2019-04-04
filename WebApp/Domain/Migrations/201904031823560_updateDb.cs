namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikeNumberProductUsers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LikeNumber = c.Int(nullable: false),
                        ProductId = c.Guid(),
                        UserId = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.ProductId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Products", "View", c => c.Int());
            AddColumn("dbo.Products", "QuantityBuy", c => c.Int());
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
            AddColumn("dbo.Users", "Age", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeNumberProductUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.LikeNumberProductUsers", "ProductId", "dbo.Products");
            DropIndex("dbo.LikeNumberProductUsers", new[] { "UserId" });
            DropIndex("dbo.LikeNumberProductUsers", new[] { "ProductId" });
            DropColumn("dbo.Users", "PhoneNumber");
            DropColumn("dbo.Users", "Age");
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Products", "QuantityBuy");
            DropColumn("dbo.Products", "View");
            DropTable("dbo.LikeNumberProductUsers");
        }
    }
}
