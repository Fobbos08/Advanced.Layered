using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IdentityServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageController : ControllerBase
    {
        private int counter = 0;

        [HttpPost]
        [Route("adduser")]
        public ActionResult AddUser(string name, string password)
        {
            Storage.Users.Add(new User(){Name = name, Password = password});
            return Ok();
        }

        [HttpPost]
        [Route("addrole")]
        public ActionResult AddRole(string name)
        {
            Storage.Roles.Add(new Role() { Name = name });
            return Ok();
        }

        [HttpPost]
        [Route("setpermissions")]
        public ActionResult SetRolePermissions(string name, AccessRights permissions)
        {
            var item = Storage.Roles.FirstOrDefault(x => x.Name.Equals(name));

            if (item == null) return NotFound();

            item.Permissions = permissions;

            return Ok();
        }

        [HttpPost]
        [Route("setroletouser")]
        public ActionResult SetRoleToUser(string userName, string roleName)
        {
            var user = Storage.Users.FirstOrDefault(x => x.Name.Equals(userName));

            if (user == null) return NotFound();

            user.RoleName = roleName;

            return Ok();
        }
    }
}
