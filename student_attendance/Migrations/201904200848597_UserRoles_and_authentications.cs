namespace student_attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserRoles_and_authentications : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        role_id = c.Int(nullable: false, identity: true),
                        role = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.role_id);
            
            CreateTable(
                "dbo.User_Role",
                c => new
                    {
                        user_role_id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        role_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.user_role_id)
                .ForeignKey("dbo.Roles", t => t.role_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.role_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Role", "user_id", "dbo.Users");
            DropForeignKey("dbo.User_Role", "role_id", "dbo.Roles");
            DropIndex("dbo.User_Role", new[] { "role_id" });
            DropIndex("dbo.User_Role", new[] { "user_id" });
            DropTable("dbo.User_Role");
            DropTable("dbo.Roles");
        }
    }
}
