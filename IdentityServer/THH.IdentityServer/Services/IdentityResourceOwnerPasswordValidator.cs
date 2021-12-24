using System.Collections.Generic;
using System.Threading.Tasks;

using Duende.IdentityServer.Validation;


using THH.IdentityServer.Models;
using THH.Shared.Core.Response;

using IdentityModel;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace THH.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            this._userManager = userManager;
        }
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            static Dictionary<string, object> errors()
            {
                return new Dictionary<string, object>
                {
                    { "Data",null },
                    { "StatusCode", StatusCodes.Status400BadRequest },
                    { "IsSuccessful", false },
                    { "ErrorData", Error.SendError(
                            path:"api/user/signin",
                            isShow:true,
                            errors:"Kullanıcı adı veya parolanız hatalı"
                        ) },
                };
            }

            ApplicationUser existUser = await _userManager.FindByNameAsync(context.UserName);
            if (existUser is null)
            {
                context.Result.CustomResponse = errors();
                return;
            }
            bool passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password);
            if (passwordCheck is false)
            {
                context.Result.CustomResponse = errors();
                return;
            }

            context.Result = new GrantValidationResult(existUser.Id, OidcConstants.AuthenticationMethods.Password);
        }
    }
}
