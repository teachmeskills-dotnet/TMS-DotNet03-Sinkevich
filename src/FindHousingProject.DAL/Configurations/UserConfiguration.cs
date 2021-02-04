using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.DAL.Configurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.UserTable)
                .HasKey(o => o.Id);
            builder.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

           // builder.Property(p => p.Email)
                //.IsRequired();

            builder.Property(p => p.Avatar)
                .HasColumnType(ConfigurationConstants.AvatarFormat);

            //builder.Property(p => p.PhoneNumber)
              //  .IsRequired();

            builder.Property(p => p.BirthDate)
                .IsRequired()
                .HasColumnType(ConfigurationConstants.DateFormat);

            builder.Property(t => t.Documents)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

            builder.Property(t => t.Gender)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

        }
    }
}
