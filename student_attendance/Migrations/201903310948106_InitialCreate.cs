namespace student_attendance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attendances",
                c => new
                    {
                        attendance_id = c.Int(nullable: false, identity: true),
                        date = c.DateTime(nullable: false),
                        entry_time = c.DateTime(nullable: false),
                        status = c.String(nullable: false),
                        student_id = c.Int(nullable: false),
                        schedule_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.attendance_id)
                .ForeignKey("dbo.Schedules", t => t.schedule_id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.student_id, cascadeDelete: true)
                .Index(t => t.student_id)
                .Index(t => t.schedule_id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        schedule_id = c.Int(nullable: false, identity: true),
                        start_time = c.DateTime(nullable: false),
                        end_time = c.DateTime(nullable: false),
                        day = c.String(nullable: false),
                        room = c.String(nullable: false),
                        class_type = c.String(nullable: false),
                        group_id = c.Int(nullable: false),
                        module_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.schedule_id)
                .ForeignKey("dbo.Groups", t => t.group_id, cascadeDelete: true)
                .ForeignKey("dbo.Modules", t => t.module_id, cascadeDelete: true)
                .Index(t => t.group_id)
                .Index(t => t.module_id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        group_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.group_id);
            
            CreateTable(
                "dbo.Modules",
                c => new
                    {
                        module_id = c.Int(nullable: false, identity: true),
                        module_code = c.String(nullable: false),
                        module_name = c.String(nullable: false),
                        credit_hour = c.String(nullable: false),
                        semester = c.String(nullable: false),
                        course_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.module_id)
                .ForeignKey("dbo.Courses", t => t.course_id, cascadeDelete: true)
                .Index(t => t.course_id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        course_id = c.Int(nullable: false, identity: true),
                        course_name = c.String(nullable: false),
                        no_of_semester = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.course_id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        student_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        gender = c.String(nullable: false),
                        email = c.String(nullable: false),
                        phone_no = c.String(nullable: false),
                        dob = c.DateTime(nullable: false),
                        enrolled_date = c.DateTime(nullable: false),
                        user_id = c.Int(),
                        course_id = c.Int(),
                        group_id = c.Int(),
                    })
                .PrimaryKey(t => t.student_id)
                .ForeignKey("dbo.Courses", t => t.course_id)
                .ForeignKey("dbo.Groups", t => t.group_id)
                .ForeignKey("dbo.Users", t => t.user_id)
                .Index(t => t.user_id)
                .Index(t => t.course_id)
                .Index(t => t.group_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        user_id = c.Int(nullable: false, identity: true),
                        user_name = c.String(nullable: false),
                        password = c.String(nullable: false),
                        type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.user_id);
            
            CreateTable(
                "dbo.Teacher_Module",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        teacher_id = c.Int(nullable: false),
                        module_id = c.Int(nullable: false),
                        teacher_type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Modules", t => t.module_id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.teacher_id, cascadeDelete: true)
                .Index(t => t.teacher_id)
                .Index(t => t.module_id);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        teacher_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        gender = c.String(nullable: false),
                        email = c.String(nullable: false),
                        phone_no = c.String(nullable: false),
                        dob = c.DateTime(nullable: false),
                        user_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.teacher_id)
                .ForeignKey("dbo.Users", t => t.user_id, cascadeDelete: true)
                .Index(t => t.user_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teacher_Module", "teacher_id", "dbo.Teachers");
            DropForeignKey("dbo.Teachers", "user_id", "dbo.Users");
            DropForeignKey("dbo.Teacher_Module", "module_id", "dbo.Modules");
            DropForeignKey("dbo.Attendances", "student_id", "dbo.Students");
            DropForeignKey("dbo.Students", "user_id", "dbo.Users");
            DropForeignKey("dbo.Students", "group_id", "dbo.Groups");
            DropForeignKey("dbo.Students", "course_id", "dbo.Courses");
            DropForeignKey("dbo.Attendances", "schedule_id", "dbo.Schedules");
            DropForeignKey("dbo.Schedules", "module_id", "dbo.Modules");
            DropForeignKey("dbo.Modules", "course_id", "dbo.Courses");
            DropForeignKey("dbo.Schedules", "group_id", "dbo.Groups");
            DropIndex("dbo.Teachers", new[] { "user_id" });
            DropIndex("dbo.Teacher_Module", new[] { "module_id" });
            DropIndex("dbo.Teacher_Module", new[] { "teacher_id" });
            DropIndex("dbo.Students", new[] { "group_id" });
            DropIndex("dbo.Students", new[] { "course_id" });
            DropIndex("dbo.Students", new[] { "user_id" });
            DropIndex("dbo.Modules", new[] { "course_id" });
            DropIndex("dbo.Schedules", new[] { "module_id" });
            DropIndex("dbo.Schedules", new[] { "group_id" });
            DropIndex("dbo.Attendances", new[] { "schedule_id" });
            DropIndex("dbo.Attendances", new[] { "student_id" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Teacher_Module");
            DropTable("dbo.Users");
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
            DropTable("dbo.Modules");
            DropTable("dbo.Groups");
            DropTable("dbo.Schedules");
            DropTable("dbo.Attendances");
        }
    }
}
