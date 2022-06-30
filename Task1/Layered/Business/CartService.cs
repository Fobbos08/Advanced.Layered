using System;
using System.Collections.Generic;
using System.Linq;
using ExpressMapper;
using ExpressMapper.Extensions;
using Task1.Business.Exceptions;
using Task1.Data.CartModule;

namespace Task1.Business
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
            Mapper.Register<Item, DbItem>();
        }

        public void AddItem(Guid cartIdentifier, Item item)
        {
            var cart = _cartRepository.GetCart(cartIdentifier);

            if (cart == null)
            {
                var cartModel = new DbCart()
                {
                    Id = cartIdentifier
                };

                var dbItem = item.Map<Item, DbItem>();
                dbItem.Quantity = 1;

                cartModel.Items.Add(dbItem);
                _cartRepository.CreateCart(cartModel);
            }
            else
            {
                var cartItem = cart.Items.SingleOrDefault(x => x.Id == item.Id);

                if (cartItem == null)
                {
                    var dbItem = item.Map<Item, DbItem>();
                    dbItem.Quantity = 1;

                    cart.Items.Add(dbItem);
                }
                else
                {
                    cartItem.Quantity++;
                }

                _cartRepository.UpdateCart(cart);
            }
        }

        public void RemoveItem(Guid cartIdentifier, int itemId)
        {
            var cart = _cartRepository.GetCart(cartIdentifier);

            if (cart == null)
            {
                throw new CartDoesNotExistException();
            }

            var cartItem = cart.Items.SingleOrDefault(x => x.Id == itemId);

            if (cartItem == null)
            {
                throw new ItemDoesNotExistException();
            }

            cartItem.Quantity--;

            if (cartItem.Quantity == 0)
            {
                cart.Items.Remove(cartItem);
            }

            _cartRepository.UpdateCart(cart);
        }

        public List<DbItem> GetItems(Guid cartIdentifier)
        {
            var cart = _cartRepository.GetCart(cartIdentifier);

            if (cart == null)
            {
                throw new CartDoesNotExistException();
            }

            return cart.Items;
        }
    }
}
