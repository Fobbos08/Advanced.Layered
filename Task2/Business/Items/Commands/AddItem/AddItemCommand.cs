using System;
using System.Threading;
using System.Threading.Tasks;
using Business.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Business.Items.Commands.AddItem
{
    public record AddItemCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Uri Image { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }
    }

    public class AddItemCommandHandler : IRequestHandler<AddItemCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public AddItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new Item()
            {
                Amount = 1,
                Category = request.Category,
                Name = request.Name,
                Description = request.Description,
                Image = request.Image,
                Price = request.Price
            };

            _context.Items.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
