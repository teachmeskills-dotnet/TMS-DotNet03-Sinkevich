using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.DAL.Configurations
{
    public class PlaceConfiguration: IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.PlaceTable)
                .HasKey(o => o.Id);
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);
            builder.Property(t => t.Description)
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);
            builder.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

            builder.HasOne(o => o.Country)
                .WithMany(t => t.Places)
                .HasForeignKey(u => u.CountryId);
        }
    }
}
