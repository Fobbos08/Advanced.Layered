using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Business.Common.Interfaces;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Business.Items.Queries.GetItem
{
    public record GetItemQuery : IRequest<Item>
    {
        public int Id { get; set; }
    }

    public class GetItemQueryHandler : IRequestHandler<GetItemQuery, Item>
    {
        private readonly IApplicationDbContext _context;

        public GetItemQueryHandler (IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Item> Handle (GetItemQuery request, CancellationToken cancellationToken)
        {
            return await _context.Items
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
