using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SleepAidTrackerApi.Models;

namespace SleepAidTrackerApi.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Supplement> Supplements { get; set; } = default!;
        public DbSet<Sleep> Sleeps { get; set; } = default!;
        public DbSet<Dose> Doses { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SleepSupplementTracker;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Dose>()
                .HasOne(d => d.Sleep)
                .WithMany(s => s.Doses)
                .HasForeignKey(d => d.SleepId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Dose>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Sleep>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Supplement>().HasData(
                new Supplement()
                {
                    Id = 1,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    Name = "L-Theanine 250mg",
                    Unit = "pills"
                },
                new Supplement()
                {
                    Id = 2,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    Name = "Glycine",
                    Unit = "grams"
                },
                new Supplement()
                {
                    Id = 3,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    Name = "Light Hygiene",
                    Unit = "minutes"
                },
                new Supplement()
                {
                    Id = 4,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    Name = "Magnesium L-Threonate",
                    Unit = "pills"
                });

            builder.Entity<Sleep>().HasData(
                new Sleep()
                {
                    Id = 5,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    TotalHours = 4.5,
                    SleepDate = new DateTime(2025, 4, 20)
                },
                new Sleep()
                {
                    Id = 6,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    TotalHours = 6,
                    SleepDate = new DateTime(2025, 4, 21)
                },
                new Sleep()
                {
                    Id = 7,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    TotalHours = 5.5,
                    SleepDate = new DateTime(2025, 4, 22)
                },
                new Sleep()
                {
                    Id = 8,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    TotalHours = 7.5,
                    DisruptionCount = 5,
                    SleepDate = new DateTime(2025, 4, 23)
                },
                new Sleep()
                {
                    Id = 9,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    TotalHours = 5,
                    DisruptionCount = 5,
                    SleepDate = new DateTime(2025, 4, 24)
                }
                );

            builder.Entity<Dose>().HasData(
                new Dose()
                {
                    Id = 10,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    SupplementId = 2,
                    SleepId = 5,
                    DoseAmount = 5,
                    DoseDate = new DateTime(2025, 4, 20)
                },
                new Dose()
                {
                    Id = 11,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    SupplementId = 3,
                    SleepId = 5,
                    DoseAmount = 30,
                    DoseDate = new DateTime(2025, 4, 20)
                },
                new Dose()
                {
                    Id = 12,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    SupplementId = 2,
                    SleepId = 6,
                    DoseAmount = 5,
                    DoseDate = new DateTime(2025, 4, 21)
                },
                new Dose()
                {
                    Id = 13,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    SupplementId = 3,
                    SleepId = 6,
                    DoseAmount = 30,
                    DoseDate = new DateTime(2025, 4, 21)
                },
                new Dose()
                {
                    Id = 14,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    SupplementId = 2,
                    SleepId = 7,
                    DoseAmount = 5,
                    DoseDate = new DateTime(2025, 4, 22)
                },
                new Dose()
                {
                    Id = 15,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    SupplementId = 3,
                    SleepId = 7,
                    DoseAmount = 60,
                    DoseDate = new DateTime(2025, 4, 22)
                },
                new Dose()
                {
                    Id = 16,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    SupplementId = 3,
                    SleepId = 8,
                    DoseAmount = 30,
                    DoseDate = new DateTime(2025, 4, 23)
                },
                new Dose()
                {
                    Id = 17,
                    UserId = "31e2241d-da6a-4825-883e-6b6bd0e37db0",
                    SupplementId = 3,
                    SleepId = 9,
                    DoseAmount = 30,
                    DoseDate = new DateTime(2025, 4, 24)
                }
                );
        }
    }
}
