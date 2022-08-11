using System;
using System.Threading;
using System.Threading.Tasks;

using Business.Common.Interfaces;

using MediatR;

namespace Business.Categories.Commands.AddCategory
{
    public record AddCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }

        public Uri Image { get; set; }

        public Domain.Entities.Category Category { get; set; }
    }

    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public AddCategoryCommandHandler (IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle (AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Category()
            {
                Name = request.Name,
                Image = request.Image,
                ParentCategory = request.Category
            };

            _context.Categories.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
