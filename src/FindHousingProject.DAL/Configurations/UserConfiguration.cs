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
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstant.UserTable)
                .HasKey(user => user.Id);

            builder.Property(user => user.FullName)
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);

            builder.Property(user => user.Role)
                .IsRequired()
                .HasMaxLength(SqlConfigurationConstant.ShortLenghtForStringField);

            builder.Property(user => user.Avatar)
                .HasColumnType(SqlConfigurationConstant.AvatarFormat);

            builder.Property(user => user.BirthDate)
                .HasColumnType(SqlConfigurationConstant.DateFormat);

            builder.Property(user => user.Documents)
                .IsRequired()
                .HasDefaultValue<string>("паспорт")
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);

            builder.Property(user => user.Gender)
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);
        }
    }
}
