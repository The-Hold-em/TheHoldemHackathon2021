using System;
using System.Collections.Generic;

namespace THH.IdentityServer.Models
{
    public class SignUpViewModel
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdentityNumber { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public DateTime DateOfBirth { get; set; }
     
    }
}
