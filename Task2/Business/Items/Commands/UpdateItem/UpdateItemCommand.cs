using System;
using System.Threading;
using System.Threading.Tasks;

using Business.Common.Exceptions;
using Business.Common.Interfaces;

using Domain.Entities;

using MediatR;

using QueueClient;

namespace Business.Items.Commands.UpdateItem
{
    public record UpdateItemCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Uri Image { get; set; }

        public decimal Price { get; set; }

        public Category Category { get; set; }

        public int Amount { get; set; }
    }

    public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly Client _queueClient;

        public UpdateItemCommandHandler (IApplicationDbContext context, Client client)
        {
            _context = context;
            _queueClient = client;
        }

        public async Task<Unit> Handle (UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (item == null)
            {
                throw new NotFoundException(nameof(Item), request.Id);
            }

            item.Category = request.Category;
            item.Price = request.Price;
            item.Amount = request.Amount;
            item.Description = request.Description;
            item.Image = request.Image;
            item.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            var updateModel = new UpdateItemModel()
            {
                Id = item.Id,
                Description = item.Description,
                Image = item.Image,
                Name = item.Name,
                Price = item.Price
            };

            _queueClient.TryPublish(QueueNames.ItemQueue, updateModel);

            return Unit.Value;
        }
    }
}
