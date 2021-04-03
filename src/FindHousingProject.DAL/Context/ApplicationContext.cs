using FindHousingProject.DAL.Configurations;
using FindHousingProject.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FindHousingProject.DAL
{
    /// <summary>
    /// Database context.
    /// </summary>
    public class ApplicationContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Housing.
        /// </summary>
        public DbSet<Housing> Housing { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options">DbContextOptions</param>
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options){
            Database.EnsureCreated();
           // Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder = builder ?? throw new ArgumentNullException(nameof(builder));

            builder.ApplyConfiguration(new CountryConfiguration());
            builder.ApplyConfiguration(new HousingConfiguration());
            builder.ApplyConfiguration(new PlaceConfiguration());
            builder.ApplyConfiguration(new ReservationConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
