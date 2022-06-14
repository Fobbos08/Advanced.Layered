using System;
using LiteDB;

namespace Task1.Data.CartModule
{
    public class CartRepository : DbProvider, ICartRepository
    {
        public CartRepository(string databaseName) : base(databaseName)
        {
        }

        public void CreateCart(DbCart cart)
        {
            Execute(database =>
            {
                var cartCollection = GetCartCollection(database);
                cartCollection.Insert(cart);
            });
        }

        public void UpdateCart(DbCart cart)
        {
            Execute(database =>
            {
                var cartCollection = GetCartCollection(database);
                cartCollection.Update(cart);
            });
        }

        public DbCart GetCart(Guid cartId)
        {
            DbCart cart = null;
            Execute(database =>
            {
                var cartCollection = GetCartCollection(database);
                cart = cartCollection.FindById(cartId);
            });

            return cart;
        }

        private ILiteCollection<DbCart> GetCartCollection(LiteDatabase db)
        {
            return db.GetCollection<DbCart>("carts");
        }
    }
}
