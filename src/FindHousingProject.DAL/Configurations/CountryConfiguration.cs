using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FindHousingProject.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for Country entity.
    /// </summary>
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstant.CountryTable)
                .HasKey(country => country.Id);

            builder.Property(country => country.Name)
                .IsRequired()
                .HasMaxLength(SqlConfigurationConstant.LongLenghtForStringField);
        }
    }
}
