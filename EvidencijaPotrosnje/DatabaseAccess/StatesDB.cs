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
        public virtual DbSet<shortStateName> shortStateNames { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<State>()
                .Property(e => e.stateName)
                .IsFixedLength();

            modelBuilder.Entity<State>()
                .HasMany(e => e.StateConsumptions)
                .WithOptional(e => e.State)
                .WillCascadeOnDelete();

            modelBuilder.Entity<StateConsumption>()
                .Property(e => e.stateCode)
                .IsFixedLength();

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

            modelBuilder.Entity<shortStateName>()
                .Property(e => e.fullName)
                .IsFixedLength();

            modelBuilder.Entity<shortStateName>()
                .Property(e => e.shortName)
                .IsFixedLength();
        }
    }
}
