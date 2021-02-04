using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.DAL.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.CountryTable)
                .HasKey(o => o.Id);
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

        }
    }
}
