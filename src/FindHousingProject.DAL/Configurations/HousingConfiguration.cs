using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FindHousingProject.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for Housing entity.
    /// </summary>
    internal class HousingConfiguration : IEntityTypeConfiguration<Housing>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Housing> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstant.HousingTable)
                .HasKey(housing => housing.Id);

            builder.Property(housing => housing.Name)
                .IsRequired()
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);

            builder.HasOne(housing => housing.Place)
                .WithMany(place => place.Housings)
                .HasForeignKey(housing => housing.PlaceId);

            builder.HasOne(housing => housing.User)
               .WithMany(user => user.Housings)
                .HasForeignKey(housing => housing.UserId);

            builder.Property(housing => housing.PricePerDay)
                .IsRequired()
                .HasColumnType(SqlConfigurationConstant.DecimalFormat)
                .HasPrecision(11, 2);

            builder.Property(housing => housing.NumberOfSeats)
                .IsRequired();

            builder.Property(housing => housing.BookedFrom)
                .HasColumnType(SqlConfigurationConstant.DateFormat);

            builder.Property(housing => housing.BookedTo)
                .HasColumnType(SqlConfigurationConstant.DateFormat);

            builder.Property(housing => housing.Description)
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);

            builder.Property(housing => housing.Address)
                .IsRequired()
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);

            builder.Property(housing => housing.Scenery)
                .HasColumnType(SqlConfigurationConstant.AvatarFormat);
        }
    }
}
