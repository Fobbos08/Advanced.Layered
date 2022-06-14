using System;

namespace Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Uri Image { get; set; }

        public Category ParentCategory { get; set; }
    }
}
