namespace student_attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AllRole_added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AllRoles",
                c => new
                    {
                        role_id = c.Int(nullable: false, identity: true),
                        role = c.String(),
                    })
                .PrimaryKey(t => t.role_id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AllRoles");
        }
    }
}
