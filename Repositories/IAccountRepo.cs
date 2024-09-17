using Finders.Models;
using Microsoft.AspNetCore.Identity;

namespace Finders.Repositories
{
    public interface IAccountRepo
    {
        Task<Finder> GetUserByEmailAsync(string email);
        Task GenerateForgotPasswordTokenAsync(Finder user);
        Task<IdentityResult> ResetPasswordAsync(ResetPassword model);
    }
}
