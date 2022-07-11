using System;

namespace QueueClient
{
    public class UpdateItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Uri Image { get; set; }

        public decimal Price { get; set; }
    }
}
