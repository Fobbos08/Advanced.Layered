using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Business.Categories.Queries.ListCategory;
using Business.Items.Queries.ListItem;

using Domain.Entities;

using HotChocolate;

using MediatR;

namespace GraphQLApi
{
    public class Query
    {
        public async Task<List<Item>> Items (ListItemQuery query, [Service] ISender sender)
        {
            var items = await sender.Send(query);
            return items;
        }

        public async Task<List<Category>> GetCategories (ListCategoryQuery query, [Service] ISender sender)
        {
            var categories = await sender.Send(query);
            return categories;
        }
    }
}
