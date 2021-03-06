
using FluentValidation;

using System;

using THH.IdentityServer.Models;

namespace THH.IdentityServer.Validations.FluentValidation
{
    public class UpdateProfileValidator : AbstractValidator<UpdateModel>
    {
        public UpdateProfileValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Kullanıcı birincil anahtar değeri boş geçilemez.");

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Parola boş geçilemez");

            RuleFor(x => x.PasswordConfirmation)
               .NotEmpty().WithMessage("Parola Doğrulama boş geçilemez")
               .Equal(x => x.Password).WithMessage("Parolalar eşleşmiyor");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad boş geçilemez")
                .MaximumLength(40).WithMessage("Ad 40 karakterden uzun olamaz");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş geçilemez")
                .MaximumLength(40).WithMessage("Soyad 40 karakterden uzun olamaz");

            RuleFor(x => x.IdentityNumber)
              .NotEmpty().WithMessage("Kimlik numarası boş geçilemez")
              .Matches("^[1-9]{1}[0-9]{9}[02468]{1}$").WithMessage("Kimlik numarası formatı hatalı");

            RuleFor(x => x.DateOfBirth)
                .InclusiveBetween(DateTime.Now.AddYears(-200), DateTime.Now.AddYears(-18))
                .WithMessage("Oy kullanmak için yeterli yaşa sahip değilsiniz");

            //RuleFor(x => x.UserName)
            //    .NotEmpty().WithMessage("Kullanıcı adı boş geçilemez");

            //RuleFor(x => x.Address)
            //    .NotEmpty().WithMessage("Adres boş geçilemez")
            //    .MaximumLength(200).WithMessage("Adres 200 karakterden uzun olamaz");

            //RuleFor(x => x.PhoneNumber)
            //    .NotEmpty().WithMessage("Telefon numarası boş geçilemez")
            //    .Matches("^(05)[0-9][0-9][1-9]([0-9]){6}$").WithMessage("Telefon numarası formatı hatalı");

            //RuleFor(x => x.Email)
            //   .NotEmpty().WithMessage("Email boş geçilemez")
            //   .EmailAddress().WithMessage("Email formatı hatalı");

            //RuleForEach(x => x.Roles)
            //  .NotNull().WithMessage("Roller boş geçilemez")
            //  .NotEmpty().WithMessage("Roller boş geçilemez");
        }
    }
}
