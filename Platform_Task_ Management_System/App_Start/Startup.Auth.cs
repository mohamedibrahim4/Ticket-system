using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using Platform_Task__Management_System.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Platform_Task__Management_System
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            CreatedRole_User();

        }
        private void CreatedRole_User()
        {
            ApplicationDbContext context = new ApplicationDbContext();


            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));


            if (!RoleManager.RoleExists(RoleName.Manager))
            {
                var roleAdmin = new IdentityRole();

                roleAdmin.Name = RoleName.Manager;
                RoleManager.Create(roleAdmin);

                var user = new ApplicationUser();
                user.Gender = (byte)GenderType.Male;
                user.UserName = "EngMostafa";
                user.Email = "EngMostafa@platform.com";
                user.ProgramName = "Eng/Mostafa";
                user.ProgramPosition = (int)Position.Manager;
                user.IsActive = true;
                //user.BirthDate = DateTime.Now;
                user.BirthDate = new DateTime(1980, 4, 7);
                user.Address = new ProgrammerAddressType()
                { Country = "Egypt", City = "mansoura", Street = "12st", BuildingNumber = "21" };

                // user.Address = new CustomerAddressType("Egypt","mansoura","125ts","ssa22");

                string userpass = "123456";
                var checkuser = UserManager.Create(user, userpass);

                if (checkuser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, RoleName.Manager);
                }




            }
            if (!RoleManager.RoleExists(RoleName.TeamLeader))
            {
                var roleManager = new IdentityRole();

                roleManager.Name = RoleName.TeamLeader;
                RoleManager.Create(roleManager);


            }



            if (!RoleManager.RoleExists(RoleName.Programmer))
            {
                var roleUser = new IdentityRole();

                roleUser.Name = RoleName.Programmer;
                RoleManager.Create(roleUser);


            }
            if (!RoleManager.RoleExists(RoleName.Employee))
            {
                var roleUser = new IdentityRole();

                roleUser.Name = RoleName.Employee;
                RoleManager.Create(roleUser);


            }
            if (!RoleManager.RoleExists(RoleName.User))
            {
                var roleUser = new IdentityRole();

                roleUser.Name = RoleName.User;
                RoleManager.Create(roleUser);


            }





        }


    }
}