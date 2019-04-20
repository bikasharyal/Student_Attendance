namespace student_attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAllRole_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserAllRoles",
                c => new
                    {
                        user_role_id = c.Int(nullable: false, identity: true),
                        user_id = c.Int(nullable: false),
                        role_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.user_role_id)
                .ForeignKey("dbo.AllRoles", t => t.role_id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id)
                .Index(t => t.role_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAllRoles", "user_id", "dbo.Users");
            DropForeignKey("dbo.UserAllRoles", "role_id", "dbo.AllRoles");
            DropIndex("dbo.UserAllRoles", new[] { "role_id" });
            DropIndex("dbo.UserAllRoles", new[] { "user_id" });
            DropTable("dbo.UserAllRoles");
        }
    }
}
