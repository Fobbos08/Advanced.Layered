using System.Threading;
using System.Threading.Tasks;
using Business.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Categories = Set<Category>();
            Items = Set<Item>();
        }

        public DbSet<Category> Categories { get; }
        public DbSet<Item> Items { get; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
