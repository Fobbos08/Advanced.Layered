using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Business.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Categories.Queries.GetCategory
{
    public record GetCategoryQuery : IRequest<Category>
    {
        public int Id { get; set; }
    }

    public class GetItemQueryHandler : IRequestHandler<GetCategoryQuery, Category>
    {
        private readonly IApplicationDbContext _context;

        public GetItemQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Where(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
