namespace student_attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Role_and_User_Role_Modules_deleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Role", "role_id", "dbo.Roles");
            DropForeignKey("dbo.User_Role", "user_id", "dbo.Users");
            DropIndex("dbo.User_Role", new[] { "user_id" });
            DropIndex("dbo.User_Role", new[] { "role_id" });
            DropTable("dbo.Roles");
            DropTable("dbo.User_Role");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.User_Role",
                c => new
                    {
                        user_role_id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        role_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.user_role_id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        role_id = c.Int(nullable: false, identity: true),
                        role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.role_id);
            
            CreateIndex("dbo.User_Role", "role_id");
            CreateIndex("dbo.User_Role", "user_id");
            AddForeignKey("dbo.User_Role", "user_id", "dbo.Users", "user_id", cascadeDelete: true);
            AddForeignKey("dbo.User_Role", "role_id", "dbo.Roles", "role_id", cascadeDelete: true);
        }
    }
}
