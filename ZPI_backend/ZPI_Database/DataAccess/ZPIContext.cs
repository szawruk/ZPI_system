using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZPI_Database.Models;

namespace ZPI_Database.DataAccess
{
    public class ZPIContext: DbContext
    {
        public ZPIContext(DbContextOptions<ZPIContext> options) : base(options)
        {

        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Foreign Key SETUP

            // User receiver and sender foreign key setup
            modelBuilder.Entity<User>()
                .HasMany(u => u.MessagesSent)
                .WithOne(ms => ms.Sender)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.MessagesReceived)
                .WithOne(ms => ms.Receiver);

            // Promoter with team foreign key setup
            modelBuilder.Entity<User>()
                .HasMany(u => u.Teams)
                .WithOne(t => t.Promoter);

            // Students with team setup foreign key setup
            modelBuilder.Entity<User>()
                .HasOne(u => u.Team)
                .WithMany(t => t.Students);

            // Unique fiels

            // User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Topic 
            modelBuilder.Entity<Topic>()
                .HasIndex(u => u.Name)
                .IsUnique();

            // Team
            modelBuilder.Entity<Team>()
                .HasIndex(u => u.Name)
                .IsUnique();

            // Token for database improvement
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Token)
                .IsUnique();

        }
    }
}
