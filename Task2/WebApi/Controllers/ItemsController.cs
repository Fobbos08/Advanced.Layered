using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Business.Items.Commands.AddItem;
using Business.Items.Commands.DeleteItem;
using Business.Items.Commands.UpdateItem;
using Business.Items.Queries.GetItem;
using Business.Items.Queries.ListItem;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApi.Models;
using KeyValuePair = WebApi.Models.KeyValuePair;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : BaseApiController
    {
        [HttpGet]
        public async Task<List<Item>> GetItems([FromQuery]ListItemQuery query)
        {
            var items = await Mediator.Send(query);
            return items;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetItem(int id)
        {
            var item = await Mediator.Send(new GetItemQuery(){ Id = id });
            return new JsonResult(new { Content = item, Links = CreateLinks(id) });
        }

        [HttpGet]
        [Route("parameters/{id:int}")]
        public async Task<ActionResult> GetItemParameters(int id)
        {
            var item = await Mediator.Send(new GetItemQuery() { Id = id });

            if (item == null) return new JsonResult(new { Content = item, Links = CreateLinks(id) });
            
            var pairs = new List<KeyValuePair>
            {
                new KeyValuePair() { Key = nameof(item.Name), Value = item.Name },
                new KeyValuePair() { Key = nameof(item.Description), Value = item.Description },
                new KeyValuePair() {Key = nameof(item.Price), Value = item.Price.ToString(CultureInfo.InvariantCulture) }
            };

            return new JsonResult(pairs);
        }

        [HttpPost]
        //[Authorize]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> CreateItem([FromBody]AddItemCommand command)
        {
            var id = await Mediator.Send(command);
            return new JsonResult(new { Content = new {ItemId = id } , Links = CreateLinks(id)});
        }

        [HttpPut]
        //[Authorize(Roles = "manager")]
        public async Task<ActionResult> UpdateItem([FromBody]UpdateItemCommand command)
        {
            await Mediator.Send(command);
            return new JsonResult(new { Content = "", Links = "" });
        }

        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult> DeleteItem(int id)
        {
            await Mediator.Send(new DeleteItemCommand(){Id = id});
            return Ok();
        }

        private List<LinkDto> CreateLinks(int id)
        {
            var list = new List<LinkDto>();
            list.Add(new LinkDto($"api/items/${id}", "delete", "DELETE"));
            list.Add(new LinkDto($"api/items/", "update", "PUT"));
            
            return list;
        }
    }
}
