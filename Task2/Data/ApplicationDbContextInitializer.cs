using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContextInitializer
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitializer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
                //await _context.SaveChangesAsync(CancellationToken.None);
            }
        }
    }
}
