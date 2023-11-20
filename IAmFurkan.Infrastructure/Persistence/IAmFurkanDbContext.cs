using IAmFurkan.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IAmFurkan.Infrastructure.Persistence;
public sealed class IAmFurkanDbContext : DbContext
{
    public IAmFurkanDbContext(DbContextOptions<IAmFurkanDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAmFurkanDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
}
