using Business.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Nutrition> Meals { get; set; }
        public DbSet<Training> TrainingSessions { get; set; }
        public DbSet<Sleep> SleepSessions { get; set; }
        public DbSet<Exercise> TrainingExercises { get; set; }
        public DbSet<TrainingExercise> TrainingSessionExercises { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
