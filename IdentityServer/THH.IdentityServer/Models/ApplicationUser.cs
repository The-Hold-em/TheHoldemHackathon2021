using Microsoft.AspNetCore.Identity;

using System;

namespace THH.IdentityServer.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IVoted { get; set; }
    }
}
