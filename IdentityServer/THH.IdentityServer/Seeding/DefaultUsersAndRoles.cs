using THH.IdentityServer.Models;
using THH.Shared.Core.StringInfo;

using Microsoft.AspNetCore.Identity;

using System.Collections.Generic;

namespace THH.IdentityServer.Seeding
{
    public static class DefaultUsersAndRoles
    {
        public static IEnumerable<SignUpViewModel> GetDevelopers()
        {
            yield return new SignUpViewModel()
            {
                UserName= "72406043028",
                Password = "Password12*",
                FirstName = "Recep",
                LastName = "Şen",
                IdentityNumber = "72406043028",
                DateOfBirth = new System.DateTime(2000, 06, 13)
                //UserName = "Daniga",
                //Email = "me@senrecep.com",
                //Address="Istanbul",
                //PhoneNumber="05319649002",
            };
        }
        public static IEnumerable<IdentityRole> GetRoles()
        {
            yield return new IdentityRole(RoleInfo.Admin);
            yield return new IdentityRole(RoleInfo.User);
        }
    }

}
