using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Experimentator.Models;
using System.Text.RegularExpressions;

namespace Experimentator.Infrastructure
{
    public class CustomPasswordValidator : IPasswordValidator<ExperimentatorUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<ExperimentatorUser> manager, ExperimentatorUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            bool IsPasswordContainsUserName = Regex.IsMatch(password.ToLower(), user.UserName.ToLower(), RegexOptions.Compiled);
            bool IsPasswordContainsSequance = Regex.IsMatch(password, "123456", RegexOptions.Compiled);

            if(IsPasswordContainsUserName)
            {
                errors.Add(new IdentityError { Code = "PasswordContainsUserName", Description = "Password can't contain username" });
            }

            if(IsPasswordContainsSequance)
            {
                errors.Add(new IdentityError { Code = "PasswordContainsSequance", Description = "Password can't contain numeric sequance" });
            }

            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }
}
