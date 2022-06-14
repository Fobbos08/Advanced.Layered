using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Common.Exceptions;
using Business.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Business.Categories.Commands.UpdateCategory
{
    public record UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Uri Image { get; set; }

        public Category Category { get; set; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (category == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            category.ParentCategory = request.Category;
            category.Image = request.Image;
            category.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
