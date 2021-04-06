using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FindHousingProject.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for Place entity.
    /// </summary>
    public class PlaceConfiguration : IEntityTypeConfiguration<Place>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstant.PlaceTable)
                .HasKey(place => place.Id);

            builder.Property(place => place.Name)
                .IsRequired()
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);

            builder.Property(place => place.Description)
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);

            builder.Property(place => place.Type)
                .IsRequired()
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);

            builder.HasOne(place => place.Country)
                .WithMany(country => country.Places)
                .HasForeignKey(place => place.CountryId);
        }
    }
}
