using Finders.Models;
using Microsoft.AspNetCore.Identity;

namespace Finders.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private readonly UserManager<Finder> _userManager;
        private readonly SignInManager<Finder> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IUserService _userService;
        //private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public async Task GenerateForgotPasswordTokenAsync(Finder user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }

        private async Task SendForgotPasswordEmail(Finder user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}",
                        string.Format(appDomain + confirmationLink, user.Email, token))
                }
            };
            //await _emailService.SendEmailForEmailConfirmation(options);
        }

        public Task<Finder> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> ResetPasswordAsync(ResetPassword model)
        {
            throw new NotImplementedException();
        }
    }
}
