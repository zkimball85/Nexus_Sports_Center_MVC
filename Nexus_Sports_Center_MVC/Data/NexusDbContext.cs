using Microsoft.EntityFrameworkCore;
using Nexus_Sports_Center_MVC.Models;

namespace Nexus_Sports_Center_MVC.Data;

public class NexusDbContext : DbContext
{
    public NexusDbContext(DbContextOptions<NexusDbContext> options) : base(options)
    {
    }

    public DbSet<Sport> Sports => Set<Sport>();

    public DbSet<Venue> Venues => Set<Venue>();

    public DbSet<Team> Teams => Set<Team>();

    public DbSet<Player> Players => Set<Player>();

    public DbSet<Game> Games => Set<Game>();

    public DbSet<GameScore> GameScores => Set<GameScore>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<Game>()
            .HasOne(g => g.HomeTeam)
            .WithMany()
            .HasForeignKey(g => g.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Game>()
            .HasOne(g => g.AwayTeam)
            .WithMany()
            .HasForeignKey(g => g.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);
    }

}

