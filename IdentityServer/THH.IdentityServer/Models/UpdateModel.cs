using System;

namespace THH.IdentityServer.Models
{
    public class UpdateModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
