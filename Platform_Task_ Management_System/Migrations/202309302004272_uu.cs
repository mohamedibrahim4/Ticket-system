namespace Platform_Task__Management_System.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchID = c.Int(nullable: false, identity: true),
                        BranchName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BranchID);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        FacilityID = c.Int(nullable: false, identity: true),
                        FacilityName = c.String(nullable: false),
                        licenseNumber = c.String(nullable: false),
                        CodeFile = c.String(nullable: false),
                        ADT = c.Int(nullable: false),
                        PPR = c.Int(nullable: false),
                        OMP = c.Int(nullable: false),
                        RAS = c.Int(nullable: false),
                        TelephoneNumber = c.String(nullable: false),
                        PasswordServer = c.String(nullable: false),
                        AnyDeskID = c.String(nullable: false),
                        AnyDeskPassword = c.String(nullable: false),
                        IsSupport = c.Boolean(nullable: false),
                        Comment = c.String(),
                        BranchID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FacilityID)
                .ForeignKey("dbo.Branches", t => t.BranchID, cascadeDelete: true)
                .Index(t => t.BranchID);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        TaskID = c.Int(nullable: false, identity: true),
                        TaskName = c.String(nullable: false),
                        TaskRequirements = c.String(nullable: false, maxLength: 4000),
                        Notes = c.String(maxLength: 4000),
                        DeadLineDate = c.DateTime(),
                        CreationDate = c.DateTime(),
                        FilePath = c.String(),
                        ContentType = c.String(),
                        FacilityID = c.Int(nullable: false),
                        applicationUserID = c.String(maxLength: 128),
                        status = c.Int(nullable: false),
                        SoftwareID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TaskID)
                .ForeignKey("dbo.AspNetUsers", t => t.applicationUserID)
                .ForeignKey("dbo.Facilities", t => t.FacilityID, cascadeDelete: true)
                .ForeignKey("dbo.Softwares", t => t.SoftwareID, cascadeDelete: true)
                .Index(t => t.FacilityID)
                .Index(t => t.applicationUserID)
                .Index(t => t.SoftwareID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Gender = c.Byte(),
                        BirthDate = c.DateTime(),
                        Address_Country = c.String(),
                        Address_City = c.String(),
                        Address_Street = c.String(),
                        Address_BuildingNumber = c.String(),
                        ProgramName = c.String(),
                        ProgramPosition = c.Int(),
                        IsActive = c.Boolean(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketID = c.Int(nullable: false, identity: true),
                        Subject = c.String(nullable: false),
                        Description = c.String(nullable: false, maxLength: 4000),
                        CreationDate = c.DateTime(),
                        FilePath = c.String(),
                        ContentType = c.String(),
                        status = c.Int(nullable: false),
                        uRGENCY = c.Int(nullable: false),
                        applicationUserID = c.String(maxLength: 128),
                        SoftwareID = c.Int(nullable: false),
                        clinkCenter = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TicketID)
                .ForeignKey("dbo.AspNetUsers", t => t.applicationUserID)
                .ForeignKey("dbo.Softwares", t => t.SoftwareID, cascadeDelete: true)
                .Index(t => t.applicationUserID)
                .Index(t => t.SoftwareID);
            
            CreateTable(
                "dbo.Softwares",
                c => new
                    {
                        SoftwareID = c.Int(nullable: false, identity: true),
                        SoftwareName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SoftwareID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Tasks", "SoftwareID", "dbo.Softwares");
            DropForeignKey("dbo.Tasks", "FacilityID", "dbo.Facilities");
            DropForeignKey("dbo.Tasks", "applicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tickets", "SoftwareID", "dbo.Softwares");
            DropForeignKey("dbo.Tickets", "applicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Facilities", "BranchID", "dbo.Branches");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tickets", new[] { "SoftwareID" });
            DropIndex("dbo.Tickets", new[] { "applicationUserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Tasks", new[] { "SoftwareID" });
            DropIndex("dbo.Tasks", new[] { "applicationUserID" });
            DropIndex("dbo.Tasks", new[] { "FacilityID" });
            DropIndex("dbo.Facilities", new[] { "BranchID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Softwares");
            DropTable("dbo.Tickets");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Tasks");
            DropTable("dbo.Facilities");
            DropTable("dbo.Branches");
        }
    }
}
