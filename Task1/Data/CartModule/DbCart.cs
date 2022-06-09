using System;
using System.Collections.Generic;

namespace Task1.Data.CartModule
{
    public class DbCart
    {
        public Guid Id { get; set; }

        public List<DbItem> Items { get; } = new List<DbItem>();
    }
}
