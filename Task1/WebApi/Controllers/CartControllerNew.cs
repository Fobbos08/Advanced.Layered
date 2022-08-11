using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Task1.Business;
using Task1.Business.Exceptions;
using Task1.Data.CartModule;

namespace WebApi.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]
    public class CartController : ControllerBase
    {
        private CartService _service;

        public CartController (CartService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<DbItem>> GetCart (string cartKey)
        {
            if (Guid.TryParse(cartKey, out Guid guid))
            {
                try
                {
                    var items = _service.GetItems(guid);
                    return items;
                }
                catch (CartDoesNotExistException e)
                {
                    return NoContent();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public ActionResult AddItem ([FromQuery] string cartKey, [FromBody] Item item)
        {
            if (Guid.TryParse(cartKey, out Guid guid))
            {
                _service.AddItem(guid, item);
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        public ActionResult DeleteItem (string cartKey, int itemId)
        {
            if (!Guid.TryParse(cartKey, out Guid guid)) return BadRequest();

            try
            {
                _service.RemoveItem(guid, itemId);
                return Ok();
            }
            catch (CartDoesNotExistException e)
            {
                return NoContent();
            }
            catch (ItemDoesNotExistException e)
            {
                return NoContent();
            }
        }
    }
}
