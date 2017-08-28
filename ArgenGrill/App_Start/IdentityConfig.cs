using Argengrill.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using RazorEngine;
using RazorEngine.Templating;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Configuration;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace ArgenGrill.Models
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage iMessage)
        {
            var HtmlResult = ChooseEmail(iMessage);

            var mailAccount = ConfigurationManager.AppSettings["ApiKey"];
            var client = new SendGridClient(mailAccount); // https://app.sendgrid.com

            var msg = new SendGridMessage()

            {
                From = new EmailAddress("diegoleivas@gmail.com", "ArgenGrill"),

                Subject = iMessage.Subject,

                PlainTextContent = iMessage.Body,

                HtmlContent = HtmlResult
            };

            msg.AddTo(new EmailAddress(iMessage.Destination));
            var response = await client.SendEmailAsync(msg);
            string status = response.StatusCode.ToString();
        }

        private static string ChooseEmail(IdentityMessage iMessage)
        {
            EmailViewModel viewModel = new EmailViewModel
            {
                ConfirmUrl = iMessage.Body
            };

            string template = "";
            var HtmlResult = "";

            switch (iMessage.Subject)
            {
                case "ArgenGrill - Confirm your account":

                    template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/EmailTemplate/WelcomeEmail.cshtml"));
                    HtmlResult = Engine.Razor.RunCompile(template, "templateKey", typeof(EmailViewModel), viewModel);
                    break;

                case "ArgenGrill - Reset Password":

                    template = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/EmailTemplate/ResetPassword.cshtml"));
                    HtmlResult = Engine.Razor.RunCompile(template, "templateKey", typeof(EmailViewModel), viewModel);
                    break;

                default:

                    HtmlResult = "";
                    break;
            }

            return HtmlResult;
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 4,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }
}