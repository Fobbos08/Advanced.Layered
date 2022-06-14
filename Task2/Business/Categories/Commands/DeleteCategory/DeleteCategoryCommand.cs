using System.Threading;
using System.Threading.Tasks;
using Business.Common.Exceptions;
using Business.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Business.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteItemCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            _context.Categories.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
