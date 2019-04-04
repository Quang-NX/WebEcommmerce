namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtabledb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CatalogCoupons",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CodeName = c.String(),
                        Decription = c.String(),
                        Amount = c.Int(),
                        StartTime = c.DateTime(),
                        EndTime = c.DateTime(),
                        ApplyForCategory = c.Guid(),
                        ApplyForProduct = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.ApplyForCategory)
                .ForeignKey("dbo.Products", t => t.ApplyForProduct)
                .Index(t => t.ApplyForCategory)
                .Index(t => t.ApplyForProduct);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        QuantityProduct = c.Int(nullable: false),
                        BuyPrice = c.Double(nullable: false),
                        OrderId = c.Guid(),
                        ProductId = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderDate = c.DateTime(),
                        ShippedDate = c.DateTime(),
                        StatusPayment = c.Int(nullable: false),
                        UserId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.CatalogCoupons", "ApplyForProduct", "dbo.Products");
            DropForeignKey("dbo.CatalogCoupons", "ApplyForCategory", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.CatalogCoupons", new[] { "ApplyForProduct" });
            DropIndex("dbo.CatalogCoupons", new[] { "ApplyForCategory" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.CatalogCoupons");
        }
    }
}
