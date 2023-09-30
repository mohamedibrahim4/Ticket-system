using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Platform_Task__Management_System.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public GenderType? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public ProgrammerAddressType Address { get; set; }
        public string ProgramName { get; set; }
        public Position? ProgramPosition { get; set; }
        public bool? IsActive { get; set; }
        //navigationProperty
        public virtual List<Task> tasks { get; set; }
        public virtual List<Ticket> tickets { get; set; }




        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<Facility> Facilities { get; set; }
        public virtual DbSet<Task> Tasks { get; set; }
        public virtual DbSet<Software> Softwares { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<Branch> Branches { get; set; }

        //public virtual DbSet<PathAndContent> PathAndContents { get; set; }

        //public virtual DbSet<FileModel> FileModels { get; set; }





    }
}