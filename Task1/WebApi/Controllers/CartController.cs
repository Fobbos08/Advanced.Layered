using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Task1.Business;
using Task1.Business.Exceptions;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CartController : ControllerBase
    {
        private CartService _service;

        public CartController (CartService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult GetCart (string cartKey)
        {
            if (Guid.TryParse(cartKey, out Guid guid))
            {
                try
                {
                    var items = _service.GetItems(guid);
                    return new JsonResult(new { cartKey = cartKey, items = items });
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
