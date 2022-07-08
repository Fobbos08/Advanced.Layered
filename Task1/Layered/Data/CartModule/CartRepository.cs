using System;
using LiteDB;
using QueueClient;

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

        public void UpdateCarts(UpdateItemModel model)
        {
            Execute(database =>
            {
                var cartCollection = GetCartCollection(database);
                foreach (var cart in cartCollection.FindAll())
                {
                    bool update = false;
                    foreach (var item in cart.Items)
                    {
                        if (item.Id == model.Id)
                        {
                            item.ImageUrl = model.Image?.ToString();
                            item.Name = model.Name;
                            item.Price = model.Price;
                            update = true;
                        }
                    }

                    if (update)
                    {
                        cartCollection.Update(cart);
                    }
                }
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
            return db.GetCollection<DbCart>("carts").Include(x=>x.Items);
        }
    }
}
