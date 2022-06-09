using System.Threading;
using System.Threading.Tasks;
using Business.Common.Exceptions;
using Business.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Business.Items.Commands.DeleteItem
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
            var entity = await _context.Items
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Item), request.Id);
            }

            _context.Items.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
