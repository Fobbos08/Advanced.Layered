using System.Threading;
using System.Threading.Tasks;

using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Business.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Category> Categories { get; }

        DbSet<Item> Items { get; }

        Task<int> SaveChangesAsync (CancellationToken cancellationToken);
    }
}
