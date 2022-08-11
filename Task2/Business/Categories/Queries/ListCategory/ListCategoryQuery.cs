using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Business.Common.Interfaces;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Business.Categories.Queries.ListCategory
{
    public record ListCategoryQuery : IRequest<List<Category>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class ListItemQueryHandler : IRequestHandler<ListCategoryQuery, List<Category>>
    {
        private readonly IApplicationDbContext _context;

        public ListItemQueryHandler (IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Handle (ListCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .Skip(request.PageSize * (request.PageNumber - 1))
                .ToListAsync(cancellationToken);
        }
    }
}
