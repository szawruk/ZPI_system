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
            modelBuilder.Entity<User>()
                .HasMany(u => u.MessagesSent)
                .WithOne(ms => ms.Sender)
                .IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(u => u.MessagesReceived)
                .WithOne(ms => ms.Receiver);
        }
    }
}
