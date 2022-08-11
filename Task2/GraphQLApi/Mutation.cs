using System.Threading.Tasks;

using Business.Categories.Commands.AddCategory;
using Business.Categories.Commands.DeleteCategory;
using Business.Categories.Commands.UpdateCategory;
using Business.Items.Commands.AddItem;
using Business.Items.Commands.DeleteItem;
using Business.Items.Commands.UpdateItem;

using HotChocolate;

using MediatR;

namespace GraphQLApi
{
    public class Mutation
    {
        public async Task<int> CreateItem (AddItemCommand command, [Service] ISender sender)
        {
            var id = await sender.Send(command);
            return id;
        }

        public async Task<Payload> UpdateItem (UpdateItemCommand command, [Service] ISender sender)
        {
            await sender.Send(command);
            return new Payload();
        }

        public async Task<Payload> DeleteItem (int id, [Service] ISender sender)
        {
            await sender.Send(new DeleteItemCommand() { Id = id });
            return new Payload();
        }

        public async Task<int> CreateCategory (AddCategoryCommand command, [Service] ISender sender)
        {
            var id = await sender.Send(command);
            return id;
        }

        public async Task<Payload> UpdateCategory (UpdateCategoryCommand command, [Service] ISender sender)
        {
            await sender.Send(command);
            return new Payload();
        }

        public async Task<Payload> DeleteCategory (int id, [Service] ISender sender)
        {
            await sender.Send(new DeleteCategoryCommand() { Id = id });
            return new Payload();
        }
    }

    public class Payload
    {
        public int Result { get; set; }
    }
}
