using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindHousingProject.DAL.Configurations
{
   public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstants.ReservationTable)
                .HasKey(o => o.Id);

            builder.HasOne(o => o.Housing)
                .WithMany(t => t.Reservations)
                .HasForeignKey(u => u.HousingId);

            builder.HasOne(o => o.User)
                .WithMany(t => t.Reservations)
                .HasForeignKey(u => u.UserId);

            builder.Property(t => t.CheckIn)
                .IsRequired()
                .HasColumnType(ConfigurationConstants.DateFormat);

            builder.Property(t => t.CheckOut)
                .IsRequired()
                .HasColumnType(ConfigurationConstants.DateFormat);

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType(ConfigurationConstants.DecimalFormat)
                .HasPrecision(11, 2);

            builder.Property(t => t.State)
                .IsRequired();

        }
    }
}
