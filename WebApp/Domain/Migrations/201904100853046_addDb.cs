namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDb : DbMigration
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
                "dbo.Categories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(maxLength: 2048),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 256),
                        PublicationDate = c.DateTime(),
                        Price = c.Double(nullable: false),
                        Description = c.String(),
                        Weight = c.Double(),
                        Height = c.Double(),
                        Width = c.Double(),
                        UrlImage = c.String(),
                        productInStock = c.Int(nullable: false),
                        View = c.Int(),
                        QuantityBuy = c.Int(),
                        CategoryId = c.Guid(),
                        SupplierId = c.Guid(),
                        ManufacturerId = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .ForeignKey("dbo.Manufacturers", t => t.ManufacturerId)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId)
                .Index(t => t.CategoryId)
                .Index(t => t.SupplierId)
                .Index(t => t.ManufacturerId);
            
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
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserName = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        Age = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(),
                        Website = c.String(maxLength: 250),
                        LogoPath = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 50),
                        Phone = c.String(nullable: false, maxLength: 20),
                        Id = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CustomerName = c.String(),
                        Address = c.String(),
                        Age = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        Cancelled = c.Int(nullable: false),
                        Deleted = c.Int(nullable: false),
                        CustomerId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.Guid(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Guid(),
                        Status = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CatalogCoupons", "ApplyForProduct", "dbo.Products");
            DropForeignKey("dbo.CatalogCoupons", "ApplyForCategory", "dbo.Categories");
            DropForeignKey("dbo.Products", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.Products", "ManufacturerId", "dbo.Manufacturers");
            DropForeignKey("dbo.LikeNumberProductUsers", "UserId", "dbo.Users");
            DropForeignKey("dbo.LikeNumberProductUsers", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.OrderDetails", new[] { "ProductId" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.LikeNumberProductUsers", new[] { "UserId" });
            DropIndex("dbo.LikeNumberProductUsers", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ManufacturerId" });
            DropIndex("dbo.Products", new[] { "SupplierId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.CatalogCoupons", new[] { "ApplyForProduct" });
            DropIndex("dbo.CatalogCoupons", new[] { "ApplyForCategory" });
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Customers");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.Users");
            DropTable("dbo.LikeNumberProductUsers");
            DropTable("dbo.Products");
            DropTable("dbo.Categories");
            DropTable("dbo.CatalogCoupons");
        }
    }
}
