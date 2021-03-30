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
    class HousingConfiguration : IEntityTypeConfiguration<Housing>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Housing> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.HousingTable)
                .HasKey(o => o.Id);
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

            builder.HasOne(o => o.Place)
                .WithMany(t => t.Housings)
                .HasForeignKey(u => u.PlaceId);

            builder.HasOne(o => o.User)
               .WithMany(t => t.Housings)
                .HasForeignKey(u => u.UserId);

            builder.Property(t => t.PricePerDay)
                .IsRequired()
                .HasColumnType(ConfigurationConstants.DecimalFormat)
                .HasPrecision(11, 2);

            builder.Property(t => t.NumberOfSeats)
                .IsRequired();

            builder.Property(t=>t.BookedFrom)
                .HasColumnType(ConfigurationConstants.DateFormat);

            builder.Property(t => t.BookedTo)
                .HasColumnType(ConfigurationConstants.DateFormat);

            builder.Property(t => t.Description)
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

            builder.Property(t => t.Address)
                .IsRequired()
                .HasMaxLength(ConfigurationConstants.LongLenghtForStringField);

            builder.Property(p => p.Scenery)
                .HasColumnType(ConfigurationConstants.AvatarFormat);
        }
    }
}
