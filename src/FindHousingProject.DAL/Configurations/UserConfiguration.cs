using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FindHousingProject.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for User entity.
    /// </summary>
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.UserTable)
                .HasKey(o => o.Id);
            builder.Property(t => t.FullName)
                //.IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

            builder.Property(t => t.Role)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.ShortLenghtForStringField);
            // builder.Property(p => p.Email)
            //.IsRequired();

            builder.Property(p => p.Avatar)
                .HasColumnType(ConfigurationConstants.AvatarFormat);

            //builder.Property(p => p.PhoneNumber)
            //  .IsRequired();

            builder.Property(p => p.BirthDate)
                // .IsRequired()
                .HasColumnType(ConfigurationConstants.DateFormat);

            builder.Property(t => t.Documents)
                .IsRequired().HasDefaultValue<string>("паспорт")
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

            builder.Property(t => t.Gender)
                // .IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);
        }
    }
}
