namespace Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abcd : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropColumn("dbo.Users", "RoleId");
            RenameColumn(table: "dbo.Users", name: "Role_Id", newName: "RoleId");
            AlterColumn("dbo.Users", "RoleId", c => c.Guid());
            CreateIndex("dbo.Users", "RoleId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "RoleId" });
            AlterColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Users", name: "RoleId", newName: "Role_Id");
            AddColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "Role_Id");
        }
    }
}
