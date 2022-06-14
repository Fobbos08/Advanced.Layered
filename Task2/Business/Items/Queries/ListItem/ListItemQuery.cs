using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Items.Queries.ListItem
{
    public record ListItemQuery : IRequest<List<Item>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class ListItemQueryHandler : IRequestHandler<ListItemQuery, List<Item>>
    {
        private readonly IApplicationDbContext _context;

        public ListItemQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> Handle(ListItemQuery request, CancellationToken cancellationToken)
        {
            return await _context.Items
                .AsNoTracking()
                .Skip(request.PageSize * (request.PageNumber - 1))
                .ToListAsync(cancellationToken);
        }
    }
}
