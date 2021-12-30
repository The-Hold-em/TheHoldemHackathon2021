﻿
using THH.IdentityServer.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace THH.IdentityServer.Mapping
{
    public class ApplicationUserMap : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(x => x.UserName).IsRequired(false);
            builder.Property(x => x.Email).IsRequired(false);
            builder.Property(x => x.PhoneNumber).IsRequired(false);
            builder.Property(x => x.UserName).IsRequired(false);
            builder.Property(x => x.FirstName).HasMaxLength(40).IsRequired(true);
            builder.Property(x => x.LastName).HasMaxLength(40).IsRequired(true);
            builder.Property(x => x.IdentityNumber).HasMaxLength(11).IsRequired(true);
            builder.HasIndex(x => x.IdentityNumber).IsUnique();
            builder.Property(x => x.DateOfBirth).IsRequired(true);
            builder.Property(x => x.IVoted).IsRequired(true);
            //builder.Property(x => x.Address).HasMaxLength(200).IsRequired(false);
        }
    }
}
