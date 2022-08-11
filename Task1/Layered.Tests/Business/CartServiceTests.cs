using System;

using ExpressMapper.Extensions;

using NSubstitute;

using NUnit.Framework;

using Task1.Business;
using Task1.Data.CartModule;

namespace Task1.Tests.Business
{
    public class CartServiceTests
    {
        private CartService _cartService;
        private ICartRepository _cartRepository;

        [SetUp]
        public void Setup ()
        {
            _cartRepository = Substitute.For<ICartRepository>();
            _cartService = new CartService(_cartRepository);
        }

        [Test]
        public void AddItem_CreateCart ()
        {
            DbCart actual = null;
            _cartRepository.CreateCart(Arg.Do<DbCart>(x => actual = x));

            var cartGuid = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);
            var item = new Item()
            {
                Id = 10,
                ImageUrl = "some url",
                Name = "name",
                Price = 12
            };

            var dbItem = item.Map<Item, DbItem>();
            dbItem.Quantity = 1;

            var expectedCart = new DbCart()
            {
                Id = cartGuid,
                Items = { dbItem }
            };

            _cartService.AddItem(cartGuid, item);

            _cartRepository.Received().CreateCart(Arg.Any<DbCart>());

            AssertCart(expectedCart, actual);
        }

        [Test]
        public void AddItem_UpdateCart_NewItem ()
        {
            var cartGuid = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            var item = new Item()
            {
                Id = 10,
                ImageUrl = "some url",
                Name = "name",
                Price = 12
            };

            var item2 = new Item()
            {
                Id = 11,
                ImageUrl = "some url1",
                Name = "name1",
                Price = 12
            };

            var dbItem = item.Map<Item, DbItem>();
            dbItem.Quantity = 1;

            var dbItem2 = item2.Map<Item, DbItem>();
            dbItem2.Quantity = 1;

            var dbCart = new DbCart()
            {
                Id = cartGuid,
                Items = { dbItem }
            };

            DbCart actual = null;
            _cartRepository.UpdateCart(Arg.Do<DbCart>(x => actual = x));
            _cartRepository.GetCart(Arg.Any<Guid>()).Returns(dbCart);

            var expectedCart = new DbCart()
            {
                Id = cartGuid,
                Items = { dbItem, dbItem2 }
            };

            _cartService.AddItem(cartGuid, item);

            _cartRepository.Received().UpdateCart(Arg.Any<DbCart>());

            AssertCart(expectedCart, actual);
        }

        [Test]
        public void AddItem_UpdateCart_ExistItem ()
        {
            var cartGuid = new Guid(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

            var item = new Item()
            {
                Id = 10,
                ImageUrl = "some url",
                Name = "name",
                Price = 12
            };

            var dbItem = item.Map<Item, DbItem>();
            dbItem.Quantity = 1;

            var dbCart = new DbCart()
            {
                Id = cartGuid,
                Items = { dbItem }
            };

            DbCart actual = null;
            _cartRepository.UpdateCart(Arg.Do<DbCart>(x => actual = x));
            _cartRepository.GetCart(Arg.Any<Guid>()).Returns(dbCart);

            var expectedCart = new DbCart()
            {
                Id = cartGuid,
                Items = { dbItem }
            };

            _cartService.AddItem(cartGuid, item);

            _cartRepository.Received().UpdateCart(Arg.Any<DbCart>());

            dbItem.Quantity = 2;

            AssertCart(expectedCart, actual);
        }

        private void AssertCart (DbCart expected, DbCart actual)
        {
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Items.Count, expected.Items.Count);

            for (var i = 0; i < expected.Items.Count; i++)
            {
                Assert.AreEqual(expected.Items[i].Quantity, expected.Items[i].Quantity);
                Assert.AreEqual(expected.Items[i].Id, expected.Items[i].Id);
                Assert.AreEqual(expected.Items[i].ImageUrl, expected.Items[i].ImageUrl);
                Assert.AreEqual(expected.Items[i].Name, expected.Items[i].Name);
                Assert.AreEqual(expected.Items[i].Price, expected.Items[i].Price);
            }
        }
    }
}
