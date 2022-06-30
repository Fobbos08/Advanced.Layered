﻿using LiteDB;

namespace Task1.Business
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public decimal Price { get; set; }
    }
}
