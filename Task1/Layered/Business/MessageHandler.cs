using QueueClient;
using Task1.Business;

namespace Layered.Business
{
    public class MessageHandler
    {
        private readonly CartService _cartService;
        private readonly Client _client;

        public MessageHandler(CartService cartService, Client client)
        {
            _cartService = cartService;
            _client = client;
        }

        public void Subscribe()
        {
            _client.Subscribe<UpdateItemModel>(QueueNames.ItemQueue, (model) =>
            {
                _cartService.UpdateItems(model);
            });
        }
    }
}
