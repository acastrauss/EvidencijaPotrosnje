using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DatabaseAccess
{
    public partial class StatesDB : DbContext
    {
        public StatesDB()
            : base("name=StatesDB")
        {
        }

        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<StateConsumption> StateConsumptions { get; set; }
        public virtual DbSet<StateWeather> StateWeathers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>()
                .Property(e => e.stateName)
                .IsFixedLength();

            modelBuilder.Entity<StateConsumption>()
                .Property(e => e.stateCode)
                .IsFixedLength();

            modelBuilder.Entity<StateConsumption>()
                .HasMany(e => e.States)
                .WithRequired(e => e.StateConsumption)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StateWeather>()
                .Property(e => e.cloudCover)
                .IsFixedLength();

            modelBuilder.Entity<StateWeather>()
                .Property(e => e.presentWeather)
                .IsFixedLength();

            modelBuilder.Entity<StateWeather>()
                .Property(e => e.recentWeather)
                .IsFixedLength();

            modelBuilder.Entity<StateWeather>()
                .Property(e => e.windDirection)
                .IsFixedLength();

            modelBuilder.Entity<StateWeather>()
                .HasMany(e => e.States)
                .WithRequired(e => e.StateWeather)
                .WillCascadeOnDelete(false);
        }
    }
}
