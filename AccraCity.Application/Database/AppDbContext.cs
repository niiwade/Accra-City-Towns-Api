using AccraCity.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AccraCity.Application.Database;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)  
    {
        modelBuilder.Entity<Region>()
            .HasMany(e => e.Districts)
            .WithOne(e => e.Region)
            .HasForeignKey(e => e.RegionId)
            .IsRequired();
        
        modelBuilder.Entity<Region>()
            .HasMany(e => e.Towns)
            .WithOne(e => e.Region)
            .HasForeignKey(e => e.RegionId)
            .IsRequired();
        
        modelBuilder.Entity<District>()
            .HasMany(e => e.Towns)
            .WithOne(e => e.District)
            .HasForeignKey(e => e.DistrictId)
            .IsRequired();
        
        modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
        modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
    }
    public DbSet<Town> Town { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<District> Districts { get; set; }
}

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // Assuming the appsettings.json file is in the same directory
            .AddJsonFile(@Directory.GetCurrentDirectory() + "/../AccraCityApi/appsettings.json")
            .Build();
        var connectionString = configuration.GetConnectionString("Default");
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        return new AppDbContext(optionsBuilder.Options);
    }
}