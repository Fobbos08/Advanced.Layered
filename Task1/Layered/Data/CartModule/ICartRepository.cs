﻿using System;

namespace Task1.Data.CartModule
{
    public interface ICartRepository
    {
        void CreateCart(DbCart cart);

        void UpdateCart(DbCart cart);

        DbCart GetCart(Guid cartId);
    }
}
