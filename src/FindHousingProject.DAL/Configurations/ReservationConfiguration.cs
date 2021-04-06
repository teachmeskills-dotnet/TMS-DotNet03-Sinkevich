using FindHousingProject.Common.Constants;
using FindHousingProject.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FindHousingProject.DAL.Configurations
{
    /// <summary>
    /// EF Configuration for Reservation entity.
    /// </summary>
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ToTable(TableConstant.ReservationTable)
                .HasKey(reservation => reservation.Id);

            builder.HasOne(reservation => reservation.Housing)
                .WithMany(housing => housing.Reservations)
                .HasForeignKey(reservation => reservation.HousingId);

            builder.HasOne(reservation => reservation.User)
                .WithMany(user => user.Reservations)
                .HasForeignKey(reservation => reservation.UserId);

            builder.Property(reservation => reservation.CheckIn)
                .IsRequired()
                .HasColumnType(SqlConfigurationConstant.DateFormat);

            builder.Property(reservation => reservation.CheckOut)
                .IsRequired()
                .HasColumnType(SqlConfigurationConstant.DateFormat);

            builder.Property(reservation => reservation.Amount)
                .IsRequired()
                .HasColumnType(SqlConfigurationConstant.DecimalFormat)
                .HasPrecision(11, 2);

            builder.Property(reservation => reservation.State)
                .IsRequired();
        }
    }
}
